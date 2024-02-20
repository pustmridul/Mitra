using Mitra.Services.Dtos.Donor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface IDonorService
    {
        Task<DonorReponse> AddDonor( int id, DonorDto donorDto);
        Task<List<DonorListDto>> GetAllDonor();
        Task<IPaginatedResponse<DonorListDto>> GetAllDonorList(int skip, int pageSize);
    }
}
