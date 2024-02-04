using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpectationController : ControllerBase
    {
        protected ResponseDto _responseDto;
        private IExpectationService _expectationService;

        public ExpectationController( IExpectationService expectationService)
        {
            this._responseDto = new ResponseDto();
            _expectationService = expectationService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddExpectation(ExpectationDto expectationDto, int id)
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


        

    }
}
