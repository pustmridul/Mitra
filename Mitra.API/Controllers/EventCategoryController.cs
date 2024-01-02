using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;

namespace Mitra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventCategoryController : ControllerBase
    {
        
        protected ResponseDto _response;
        private readonly IEventCategoryService _eventCategoryService;

        public EventCategoryController(IEventCategoryService eventCategoryService)
        {
            _eventCategoryService = eventCategoryService;
            this._response = new ResponseDto();
        }
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<EventCategoryDTO> eventCategories = await _eventCategoryService.GetAllEventCategory();
                _response.Result = eventCategories;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateEventCategory(EventCategoryDTO eventCategoryDTO)
        {
            try
            {
                var result = await _eventCategoryService.AddEventCategory(eventCategoryDTO);
                _response.Result = result;
            }
            catch(Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<object>> UpdateEventCategory(int id, EventCategoryDTO eventCategoryDTO)
        {
            try
            {
                var result = await _eventCategoryService.UpdateEventCategory(id, eventCategoryDTO);
                _response.Result = result;
            }
            catch( Exception ex)
            { 
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
       
        }

    }
}
