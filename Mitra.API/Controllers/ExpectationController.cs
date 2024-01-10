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

        public ExpectationController(ResponseDto responseDto, IExpectationService expectationService)
        {
            _responseDto = responseDto;
            _expectationService = expectationService;
        }


        

    }
}
