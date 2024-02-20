using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Services.Dtos.Common;
using Mitra.Services.Dtos.Event;
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
        public async Task<ActionResult<object>> GetEvent()
        {
            try
            {
                IEnumerable<EventDTO> events = await _eventService.GetAllEvent();
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
        public async Task<ActionResult<object>> CreateEvent(int id, EventDTO eventDTO)
        {
            try
            {
                var result = await _eventService.AddEvents(id, eventDTO);
                _responseDto.Result = result;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _responseDto;
        }

        [HttpGet("GetAllEvent")]
        public async Task<ActionResult<object>> GetAllEvent(int page, int pageSize)
        {
            try
            {
                int skip = (page - 1) * pageSize;
                var result = await _eventService.GetEvents(page, pageSize);
                _responseDto.Count = result.TotalRecords;
                _responseDto.Result = result.Data;
                _responseDto.IsSuccess = true;
                _responseDto.DisplayMessage = "Load ALL Data";

            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<object>> GetById(int eventId)
        {
            try
            {
                var data = await _eventService.GetById(eventId);
                _responseDto.Result = data;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
            }
            return _responseDto;

        }

        //    [HttpGet("GetByCatId")]
        //    public async Task<ActionResult<object> GetByCategoryId(int eventCategory)
        //    {
        //      try{
        //        }catch(Exception ex)
        //        {
        //        }
        //return null;
        //    }
        [HttpGet("GetByCatId")]
        public async Task<ActionResult<object>> GetByCatId( int CategoryId)
        {
            try
            {
                var result = await _eventService.GetByCatId(CategoryId);
                _responseDto.Result = result;
            }catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }


        [HttpDelete("Delete")]
        public async Task<ActionResult<object>> GetDeleteById(int eventId)
        {
            try
            {
                var data = await _eventService.DeleteById(eventId);
                _responseDto.Result = data;

            }catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
            }
            return _responseDto;
        }

        


    }
}
