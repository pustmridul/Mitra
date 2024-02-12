using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Services
{
    public class ExpectationService : IExpectationService
    {
        private IMapper _mapper;
        private AppDbContext _appDbContext;
        public ExpectationService(IMapper mapper, AppDbContext appDbContext) 
        { 
            _mapper = mapper;
            _appDbContext = appDbContext;
        }
        public async Task<List<ExpectationDto>> AddExpectation(ExpectationDto expectationDto, int id)
        {
            var expectationfond = await _appDbContext.Expectations.FindAsync(id);
           
            if(expectationfond == null)
            {              
                var expectationExists = await _appDbContext.Expectations
                   .FirstOrDefaultAsync(e => e.EventId == expectationDto.EventId && e.DonorId == expectationDto.DonorId);
                if (expectationExists == null)
                {
                    var expection = _mapper.Map<Expectation>(expectationDto);
                    _appDbContext.Add(expection);
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {                  
                    throw new InvalidOperationException("Expectation already exists for this donor and event.");
                }
            }
            else
            {
                _mapper.Map(expectationDto, expectationfond);
                await _appDbContext.SaveChangesAsync();
            }

           


            var expectationUp = await _appDbContext.Expectations
               .ProjectTo<ExpectationDto>(_mapper.ConfigurationProvider)
               .ToListAsync();

            return expectationUp;
        }

        public async Task<ExpectationDto> GetExpByEventAndDonorId(int donorId, int eventId)
        {
            var result = await _appDbContext.Expectations
                .FirstOrDefaultAsync(e => e.DonorId == donorId && e.EventId == eventId);

            return _mapper.Map<ExpectationDto>(result);
            
        }

        public async Task<List<ExpectationDto>> GetNOtDonateYetByEventId(int eventId)
        {
            var query = from e in _appDbContext.Expectations
                        join d in _appDbContext.Donors on e.DonorId equals d.Id
                        join ev in _appDbContext.Events on e.EventId equals ev.Id
                        into eventGroup
                        from ev in eventGroup.DefaultIfEmpty()
                        join don in _appDbContext.Donations
                            on new { e.EventId, e.DonorId } equals new { don.EventId, don.DonorId } into donationGroup
                        from don in donationGroup.DefaultIfEmpty()
                        where  don.EventId == eventId
                        select new ExpectationDto
                        {
                            EventId = e.EventId,
                            EventName = ev.EventName,
                            DonorId = e.DonorId,
                            DonorName = d.Name,
                            Amount = e.Amount
                        };

            var result = await query.ToListAsync();

            return result;
        }

        //public Task<List<ExpectationDto>> GetNOtDonateYetByEventId(int eventId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
