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

        public async Task<DonorReponse> AddDonor(int id, DonorDto donorDto)
        {
                var result = new DonorReponse();
                var exitDonor = await _appDbContext.Donors.FindAsync(id);
                if (exitDonor != null)
                {
                    _mapper.Map(donorDto, exitDonor);
                }
                else
                {
                var existingDonor = await _appDbContext.Donors.AnyAsync(e => e.MobileNumber == donorDto.MobileNumber);
                if (existingDonor != false)
                {
                    result.Message = "Donor Exit";
                    return result;
                }
                else
                {
                    var newDonor = _mapper.Map<Donor>(donorDto);
                    _appDbContext.Add(newDonor);
                }

               
                }

                await _appDbContext.SaveChangesAsync();

                var newDonors = await _appDbContext.Donors
                    .ProjectTo<DonorDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();
                result.Message = "Donor Add Succefully";
                return result;
           
        }


        public async Task<List<DonorListDto>> GetAllDonor()
        {
            //var donorlist = await _appdbcontext.donors.tolistasync();
            //var donorlistdtos = _mapper.map<list<donorlistdto>>(donorlist);
            List<Donor> donors = await _appDbContext.Donors.ToListAsync();

            var donorlistdtos  = _mapper.Map<List<DonorListDto>>(donors);
            return donorlistdtos;

          
        }


        public class PaginatedResponse<T> : IPaginatedResponse<T>
        {
            public IEnumerable<T> Data { get; set; }
            public int TotalRecords { get; set; }
        }
        public async Task<IPaginatedResponse<DonorListDto>> GetAllDonorList(int skip, int take)
        {
            try
            {

                skip = Math.Max(skip, 0);
                take = Math.Max(take, 0);

                var totalRecords = await GetTotalDonorCount();
                var donors = await GetPaginatedDonor(skip, take);


                var response = new PaginatedResponse<DonorListDto>
                {
                    Data = donors,
                    TotalRecords = totalRecords
                };

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception("Error retrieving paginated event categories", ex);
            }
        }

        private async Task<int> GetTotalDonorCount()
        {

            return await _appDbContext.Donors.CountAsync();
        }

        private async Task<IEnumerable<DonorListDto>> GetPaginatedDonor(int skip, int take)
        {

            return await _appDbContext.Donors
                .Skip(skip)
                .Take(take)
                .Select(donors => _mapper.Map<DonorListDto>(donors))
                .ToListAsync();

            //try
            //{
            //    var donors = await _appDbContext.Donors
            //        .Skip(skip)
            //        .Take(take)
            //        .ToListAsync();

            //    // Check if donors is not null
            //    if (donors != null)
            //    {
            //        // Map Donors to DonorListDto
            //        var donorListDto = donors.Select(donor => _mapper.Map<DonorListDto>(donor)).ToList();

            //        // Return the mapped list
            //        return donorListDto;
            //    }
            //    else
            //    {
            //        // Handle the case where donors is null
            //        // Perhaps return an empty list or throw an exception
            //        return new List<DonorListDto>();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Handle exceptions appropriately
            //    // Log the exception and possibly rethrow it or return an error response
            //    Console.WriteLine($"Error retrieving donors: {ex.Message}");
            //    throw;
            //}
        }



    }
}
