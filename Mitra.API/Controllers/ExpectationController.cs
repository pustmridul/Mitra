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
    public class ExpectationController : ControllerBase
    {
        protected ResponseDto _responseDto;
        private IExpectationService _expectationService;

        public ExpectationController(IExpectationService expectationService)
        {
            this._responseDto = new ResponseDto();
            _expectationService = expectationService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddExpectation(ExpectationAddDto expectationDto, int id)
        {
            try
            {
                var ressult = await _expectationService.AddExpectation(expectationDto, id);
                _responseDto.Result = ressult;

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.Message };

            }

            return _responseDto;
        }

        [HttpGet]
        public async Task<ActionResult<object>> GetExpByEventAndDonorId(int donorId, int eventId)
        {
            try {
                var result = await _expectationService.GetExpByEventAndDonorId(donorId, eventId);
                _responseDto.Result = result;

            }
            catch (Exception ex)
            {
                _responseDto.Result = false;
            }
            return _responseDto;
        }

        [HttpGet("GetExpectationList")]
        public async Task<ActionResult<object>> GetExpectationList(int page , int pageSize)
        {
            try
            {
                int skip = (page - 1) * pageSize;


                var paginatedResponse = await _expectationService.GetExpectationList(skip, pageSize);


                _responseDto.Result = paginatedResponse.Data;
                _responseDto.Count = paginatedResponse.TotalRecords;
                _responseDto.IsSuccess = true;
                _responseDto.DisplayMessage = "Load ALL Data";
            }
            catch(Exception ex)
            {
                _responseDto = new ResponseDto();
            }
            return _responseDto;
        }

        [HttpGet("GetExpectation")]
        public async Task<ActionResult<object>> GetNOtDonateYetByEventId(int eventId)
        {
            try{
                var result = await _expectationService.GetNOtDonateYetByEventId(eventId); 
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

    }
}
