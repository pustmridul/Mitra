using Mitra.Domain.Entity;
using Mitra.Services.Dtos.Donation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface IDonationService
    {
        Task<List<DonationDto>> AddDoation(DonationDto donationDto);
        Task<IPaginatedResponse<DonationListDto>> GetDonation(int page, int pageSize);
    }
}
