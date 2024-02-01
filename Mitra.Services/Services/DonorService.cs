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

        public async Task<List<DonorDto>> AddDonor(int id, DonorDto donorDto)
        {

            var exitdonor = await _appDbContext.Donors.FindAsync(id);
            if (exitdonor == null)
            {
                var donor = await _appDbContext.Donors.FirstOrDefaultAsync(u => u.MobileNumber == donorDto.MobileNumber);
                if (donor != null)
                {                 
                    return new List<DonorDto> { new DonorDto { MobileNumber = "Donor Exists" } };

                }
                var donors = _mapper.Map<Donor>(donorDto);
                _appDbContext.Add(donors);
                await _appDbContext.SaveChangesAsync();

            }
            _mapper.Map(donorDto, exitdonor);
            await _appDbContext.SaveChangesAsync();

            var newDonors = await _appDbContext.Donors
                .ProjectTo<DonorDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return newDonors;

        }
    }
}
