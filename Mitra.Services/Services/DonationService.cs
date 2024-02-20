using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Common;
using Mitra.Services.Dtos.Donation;
using Mitra.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Services
{
    public class DonationService : IDonationService
    {
        private IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public DonationService(AppDbContext appDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }   

        public async Task<List<DonationDto>> AddDoation(DonationDto donationDto)
        {
            var donation = _mapper.Map<Donation>(donationDto);
            _appDbContext.Add(donation);
            await _appDbContext.SaveChangesAsync();

            var newDonation = await _appDbContext.Donations
                .ProjectTo<DonationDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return newDonation;
        }

        public async Task<IPaginatedResponse<DonationListDto>> GetDonation(int skip, int take)
        {
            try
            {

                skip = Math.Max(skip, 0);
                take = Math.Max(take, 0);

                var totalRecords = await GetTotalEventCategoryCount();
                var eventCategories = await GetPaginatedEventCategories(skip, take);

                var response = new PaginatedResponse<DonationListDto>
                {
                    Data = eventCategories,
                    TotalRecords = totalRecords
                };

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception("Error retrieving paginated event categories", ex);
            }
        }

        private async Task<int> GetTotalEventCategoryCount()
        {

            return await _appDbContext.Donations.CountAsync();
        }

        private async Task<IEnumerable<DonationListDto>> GetPaginatedEventCategories(int skip, int take)
        {

            return await _appDbContext.Donations
        .Join(
            _appDbContext.Events,
            donation => donation.EventId,
            @event => @event.Id,
            (donation, @event) => new { Donation = donation, Event = @event }
        )
        .Join(
            _appDbContext.Donors,
            join1 => join1.Donation.DonorId,
            donor => donor.Id,
            (join1, donor) => new { join1.Donation, join1.Event, Donor = donor }
        )
        .OrderByDescending(join => join.Donation.Id) // Order by some suitable property, e.g., Id
        .Skip(skip)
        .Take(take)
        .Select(join => new DonationListDto
        {
            Amount = join.Donation.Amount,
            EventName = join.Event.EventName,
            DonorName = join.Donor.Name
        })
        .ToListAsync();
        }

       
    }
}
