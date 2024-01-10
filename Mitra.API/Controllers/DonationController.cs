using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        protected ResponseDto _responseDto;
        private readonly IDonationService _donationService;

        public DonationController(IDonationService donationService)
        {
            this._responseDto = new ResponseDto();
            _donationService = donationService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddDonation(DonationDto donationDto)
        {
            try
            {
                var res = await _donationService.AddDoation(donationDto);
                _responseDto.Result = res;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }
       
           
            

    }
}
