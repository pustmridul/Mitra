using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using Mitra.Services.Services;

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
        public async Task<ActionResult<object>> AddDonor(int id, DonorDto donorDto)
        {
            try
            {
                var result = await _donorService.AddDonor(id, donorDto);
                _responseDto.Result = result; 
               

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.Message };

            }
            return _responseDto;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetAllDonor()
        {
            try
            {
                List<DonorListDto> donors = await _donorService.GetAllDonor(); 
                _responseDto.Result = donors;
                 
            }catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
            }

            return _responseDto;
        }
        [HttpGet("GetALL")]
        public async Task<object> GetDonorList(int page, int pageSize)
        {
            try
            {
              
                int skip = (page - 1) * pageSize;
                var paginatedResponse = await _donorService.GetAllDonorList(skip, pageSize);

                _responseDto.Result = paginatedResponse.Data;
                _responseDto.Count = paginatedResponse.TotalRecords;
                _responseDto.IsSuccess = true;
                _responseDto.DisplayMessage = "Load ALL Data";
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

    }
}
