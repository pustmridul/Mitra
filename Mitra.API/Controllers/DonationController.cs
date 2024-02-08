using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using static Mitra.Services.Services.EventCategoryService;

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

        [HttpGet]
        public async Task<ActionResult<object>> GetDonation(int page, int pageSize)
        {
            try
            {
                int skip = (page - 1) * pageSize;
                var res = await _donationService.GetDonation(skip, pageSize);
                _responseDto.Result = res.Data;
                _responseDto.Count = res.TotalRecords;
                _responseDto.IsSuccess = true;
                _responseDto.DisplayMessage = "Load ALL Data";

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }    

    }
}
