using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mitra.Domain;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services.Services
{
    public  class EventService:IEventService
    {

        private IMapper _mapper;
        private AppDbContext _db;
        public EventService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _db = context;
        }

        public async Task<List<EventDTO>> AddEvents(EventDTO eventDTO)
        {
            var events = _mapper.Map<Event>(eventDTO);
            _db.Add(events);
            await _db.SaveChangesAsync();

            var newEvents = await _db.Events
                .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return newEvents;

        }

        public async Task<IEnumerable<EventDTO>> GetAllEvent()
        {
            List<Event> events = await _db.Events.ToListAsync();
            return _mapper.Map<List<EventDTO>>(events);

        }
    }
}
