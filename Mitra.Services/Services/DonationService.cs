using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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
    }
}
