using Mitra.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public  interface IEventCategoryService
    {
        Task<IEnumerable<EventCategoryDTO>> GetAllEventCategory();
        Task<List<EventCategoryDTO>> AddEventCategory(EventCategoryDTO eventCategoryDTO);
        Task<EventCategoryDTO?> GetEventCategoryById(int id);
        Task<List<EventCategoryDTO>> UpdateEventCategory(int id, EventCategoryDTO requst);
        Task<List<EventCategoryDTO>> DeleteEventCategory(int id);

    }
}
