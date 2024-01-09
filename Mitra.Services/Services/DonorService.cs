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
    public class DonorService : IDonorService
    {
        private IMapper _mapper;
        private AppDbContext _appDbContext;

        public DonorService(IMapper mapper, AppDbContext appDbContext)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
        }   

        public async Task<List<DonorDto>> AddDonor(DonorDto donorDto)
        {
            var donor = await _appDbContext.Donors.FirstOrDefaultAsync(u => u.MobileNumber == donorDto.MobileNumber);

            if(donor != null)
            {
                // Return a list with a single "Donor Exists" item or handle it according to your needs
                return new List<DonorDto> { new DonorDto { MobileNumber = "Donor Exists" } };

            }

            var donors = _mapper.Map<Donor>(donorDto);
            _appDbContext.Add(donors);
            await _appDbContext.SaveChangesAsync();

            var newDonors = await _appDbContext.Donors
                .ProjectTo<DonorDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return newDonors;

        }
    }
}
