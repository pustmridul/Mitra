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
        public async Task<object> Get(int page, int pageSize)
        {
            try
            {
               
                int skip = (page - 1) * pageSize;
                

                var paginatedResponse = await _eventCategoryService.GetAllEventCategory(skip, pageSize);

               
                _response.Result = paginatedResponse.Data;
                _response.Count = paginatedResponse.TotalRecords;
                _response.IsSuccess = true;
                _response.DisplayMessage = "Load ALL Data";
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

        [HttpDelete]
        public async Task<ActionResult<object>> DeleteEventCategory(int eventId)
        {
            try
            {
                var result = await _eventCategoryService.DeleteEventCategory(eventId);  
                _response.Result = result;
                _response.DisplayMessage = "Delete Successgully";
            }
            catch(Exception ex)
            {
                _response.IsSuccess=false;  
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
                _response.DisplayMessage = "DATA ADDED or UPDATE Sucessfully";
               
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
