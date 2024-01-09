using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        protected ResponseDto _responseDto;
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            this._responseDto = new ResponseDto();
            _donorService = donorService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddDonor(DonorDto donorDto)
        {
            try
            {
                var result = await _donorService.AddDonor(donorDto);
                _responseDto.Result = result;    

            }
            catch (Exception ex)
            {
                var message = ex.Message;
                
            }
            return _responseDto;
        }
    }
}
