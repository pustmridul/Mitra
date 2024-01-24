using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using Mitra.Services.Services;

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
       // public async Task<ActionResult<object>> Get()
        public async Task<object> Get(int page, int pageSize)
        {
            //var response = new ResponseDto();
            try
            {
                //IEnumerable<EventCategory> eventCategory = await _eventCategoryService.GetAllEventCategory();
                //_responseDto.Result = eventCategory;

                // Calculate the number of items to skip based on the page number and page size
                int skip = (page - 1) * pageSize;
                //IEnumerable<EventCategoryDTO> eventCategories = await _eventCategoryService.GetAllEventCategory(skip, pageSize);

                var paginatedResponse = await _eventCategoryService.GetAllEventCategory(skip, pageSize);

                // Set the Result, Count, and IsSuccess properties in the response
                _response.Result = paginatedResponse.Data;
                _response.Count = paginatedResponse.TotalRecords;
                _response.IsSuccess = true;
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

        [HttpGet("EventCategoryId")]
        public async Task<ActionResult<object>> EventCategoryFindById(int EventCategoryId)
        {
            try
            {
                var result = await _eventCategoryService.GetEventCategoryById(EventCategoryId);
                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
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
