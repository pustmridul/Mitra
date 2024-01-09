using Mitra.Services.Dtos;
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
    }
}
