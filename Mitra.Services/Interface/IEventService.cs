using Mitra.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Interface
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllEvent();
        Task<IPaginatedResponse<EventListDto>> GetEvents(int skip, int pageSize);
        Task<List<EventDTO>> AddEvents(EventDTO eventDTO);
        Task<List<EventDTO>> UpdateEvents(EventDTO eventDTO);
        Task<EventListDto?> GetById(int id);
    }
}
