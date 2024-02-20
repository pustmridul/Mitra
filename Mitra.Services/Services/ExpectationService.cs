using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Common;
using Mitra.Services.Dtos.Expectation;
using Mitra.Services.Interface;


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
        public async Task<List<ExpectationAddDto>> AddExpectation(ExpectationAddDto expectationDto, int id)
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
               .ProjectTo<ExpectationAddDto>(_mapper.ConfigurationProvider)
               .ToListAsync();

            return expectationUp;
        }

        public async Task<ExpectationDto> GetExpByEventAndDonorId(int donorId, int eventId)
        {
            var result = await _appDbContext.Expectations
                .FirstOrDefaultAsync(e => e.DonorId == donorId && e.EventId == eventId);

            return _mapper.Map<ExpectationDto>(result);
            
        }

        public async Task<IPaginatedResponse<ExpectationDto>> GetExpectationList(int skip, int take)
        {
            try
            {

                skip = Math.Max(skip, 0);
                take = Math.Max(take, 0);

                var totalRecords = await GetTotalExpectationCount();
                var expectations = await GetPaginatedExpectation(skip, take);

                var response = new PaginatedResponse<ExpectationDto>
                {
                    Data = expectations,
                    TotalRecords = totalRecords
                };

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception("Error retrieving paginated event categories", ex);
            }
        }

        private async Task<int> GetTotalExpectationCount()
        {

            return await _appDbContext.Expectations.CountAsync();
        }

        private async Task<IEnumerable<ExpectationDto>> GetPaginatedExpectation(int skip, int take)
        {

            var query = (from e in _appDbContext.Expectations
                         join d in _appDbContext.Donors on e.DonorId equals d.Id
                         join ev in _appDbContext.Events on e.EventId equals ev.Id
                         select new ExpectationDto
                         {
                             EventId = e.EventId,
                             EventName = ev.EventName,
                             DonorId = e.DonorId,
                             DonorName = d.Name,
                             Amount = e.Amount
                         })
             .Skip(skip)
             .Take(take);
            var result = await query.ToListAsync();
            return result;

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
                            //where don == null && e.EventId == (string.IsNullOrEmpty(eventId) ? e.EventId : eventId)
                        where don == null && e.EventId == eventId
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
