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
       private readonly DonationService _donationService;

        public Task<List<DonationDto>> AddDoation(DonationDto donationDto)
        {
            throw new NotImplementedException();
        }
    }
}
