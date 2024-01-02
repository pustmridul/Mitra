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
    public class EventController : ControllerBase
    {
        protected ResponseDto _responseDto;
        public readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            this._responseDto = new ResponseDto();
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<ActionResult<object>>  GetEvent()
        {
            try
            {
                IEnumerable<EventDTO> events =  await _eventService.GetAllEvent();
                _responseDto.Result = events;
                
            }
            catch (Exception ex) 
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _responseDto;
        }

        //[HttpPost("add")]
        //public async Task<ActionResult<List<EventDTO>>> AddEvent([FromBody] EventDTO eventDTO)
        //{
        //    var newEvents = await _yourService.AddEvents(eventDTO);
        //    return Ok(newEvents);
        //}

        [HttpPost]
        public async Task<ActionResult<object>> CreateEvent(EventDTO eventDTO)
        {
            try
            {
                var result = await _eventService.AddEvents(eventDTO);
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _responseDto;
        }


    }
}
