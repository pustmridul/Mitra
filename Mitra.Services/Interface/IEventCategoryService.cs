using Microsoft.AspNetCore.Mvc.RazorPages;
using Mitra.Services.Dtos.EventCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public  interface IEventCategoryService
    {
        Task<List<EventCategoryListDTO>> GetAllEventCategory();
        Task<IPaginatedResponse<EventCategoryListDTO>> GetAllEventCategory(int page, int pageSize);
        Task<List<EventCategoryDTO>> AddEventCategory(EventCategoryDTO eventCategoryDTO);
        Task<EventCategoryListDTO?> GetEventCategoryById(int id);
        Task<List<EventCategoryDTO>> UpdateEventCategory(int id, EventCategoryDTO requst);
        Task<List<EventCategoryDTO>> DeleteEventCategory(int id);

    }
}
