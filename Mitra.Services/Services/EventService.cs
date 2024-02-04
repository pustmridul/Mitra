using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Mitra.Services.Services.EventCategoryService;

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

        public async Task<List<EventDTO>> AddEvents(int id, EventDTO eventDTO)
        {
            var res = await _db.Events.FindAsync(id);
            if(res is null)
            {
                var eventss = _mapper.Map<Event>(eventDTO);
                _db.Add(eventss);
                await _db.SaveChangesAsync();
            }
            //var events = _mapper.Map<Event>(eventDTO);

            //_db.Add(events);
            //await _db.SaveChangesAsync();
            _mapper.Map(eventDTO, res);

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

        public async Task<IPaginatedResponse<EventListDto>> GetEvents(int skip, int take)
        {
            try
            {

                skip = Math.Max(skip, 0);
                take = Math.Max(take, 0);

                var totalRecords = await GetTotalEventCount();
                var eventCategories = await GetPaginatedEvent(skip, take);

                var response = new PaginatedResponse<EventListDto>
                {
                    Data = eventCategories,
                    TotalRecords = totalRecords
                };

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception("Error retrieving paginated event categories", ex);
            }
        }

        private async Task<int> GetTotalEventCount()
        {

            return await _db.Events.CountAsync();
        }

        private async Task<IEnumerable<EventListDto>> GetPaginatedEvent(int skip, int take)
        {

            return await _db.Events
                .Skip(skip)
                .Take(take)
                 .Join(_db.EventCategories,
              e => e.EventcategoryId,
              ec => ec.Id,
              (e, ec) => new EventListDto
              {
                  Id = e.Id,
                  EventName = e.EventName,
                  EventAddress = e.EventAddress,
                  StartDate = e.StartDate,
                  EndDate = e.EndDate,
                  EventCategoryName = ec.CategoryName
              })
        .ToListAsync();
        }

        public Task<List<EventDTO>> UpdateEvents(EventDTO eventDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<EventListDto?> GetById(int id)
        {
            //var eventdata = await _db.Events.FindAsync(id);

            //if (eventdata == null)
            //    return null;

            //var eventCategoryDTO = _mapper.Map<EventListDto>(eventdata);

            //return eventCategoryDTO;
            var eventCategoryDto = await _db.Events
        .Where(e => e.Id == id)
        .Join(
            _db.EventCategories,
            e => e.EventcategoryId,
            ec => ec.Id,
            (e, ec) => new EventListDto
            {
                Id = e.Id,
                EventName = e.EventName,
                EventAddress = e.EventAddress,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                EventCategoryName = ec.CategoryName
            }
        )
        .FirstOrDefaultAsync();

            return eventCategoryDto;

        }

        public async Task<List<EventDTO>> DeleteById(int id)
        {
            var eventsss = await _db.Events.FindAsync(id);
            if (eventsss is null)

                return null;
            _db.Events.Remove(eventsss);
            await _db.SaveChangesAsync();

            var updatedEvent = await _db.Events
               .ProjectTo<EventDTO>(_mapper.ConfigurationProvider)
               .ToListAsync();

            return updatedEvent;
        }

        public async Task<List<EventListDto>> GetByCatId(int catId)
        {
            var eventList = await _db.Events
                .Where(e => e.EventcategoryId == catId)
                    .ToListAsync();

            var eventListDto = _mapper.Map<List<EventListDto>>(eventList);

            return eventListDto;
        }

        //    var query = from e in _db.Events
        //                join ec in _db.EventCategories on e.EventcategoryId equals ec.Id
        //                select new EventListDto
        //                {
        //                    EventName = e.EventName,
        //                    EventAddress = e.EventAddress,
        //                    StartDate = e.StartDate,
        //                    EndDate = e.EndDate,
        //                    EventCategoryName = ec.CategoryName
        //                };

        //return await query
        //    .Skip(skip)
        //    .Take(take)
        //    .ToListAsync();

    }
}
