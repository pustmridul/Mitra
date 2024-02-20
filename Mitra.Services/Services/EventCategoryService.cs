using AutoMapper;
using Mitra.Domain.Entity;
using Mitra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mitra.Services.Interface;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using Mitra.Services.Dtos.EventCategory;
using Mitra.Services.Common;

namespace Mitra.Services.Services
{
    public  class EventCategoryService :IEventCategoryService
    {
        private IMapper _mapper;
        private AppDbContext _db;
        public EventCategoryService(IMapper mapper, AppDbContext context)
        {
            _mapper = mapper;
            _db = context;
        }

        public async  Task<List<EventCategoryDTO>> AddEventCategory(EventCategoryDTO eventCategoryDTO)
        {
            var eventCategory = _mapper.Map<EventCategory>(eventCategoryDTO);

            _db.EventCategories.Add(eventCategory);
            await _db.SaveChangesAsync();

            // Fetch updated list of EventCategoryDTO using AutoMapper
            var updatedEventCategories = await _db.EventCategories
                .ProjectTo<EventCategoryDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return updatedEventCategories;
        }

        public async Task<List<EventCategoryDTO>> DeleteEventCategory(int id)
        {
            var eventCategory = await _db.EventCategories.FindAsync(id);
            if (eventCategory is null)

                return null;
           _db.EventCategories.Remove(eventCategory);
            await _db.SaveChangesAsync();
            var updatedEventCategories = await _db.EventCategories
               .ProjectTo<EventCategoryDTO>(_mapper.ConfigurationProvider)
               .ToListAsync();

            return updatedEventCategories;

        }


          public async Task<IPaginatedResponse<EventCategoryListDTO>> GetAllEventCategory(int skip, int take)
        {
            try
            {
               
                skip = Math.Max(skip, 0);
                take = Math.Max(take, 0);

                var totalRecords = await GetTotalEventCategoryCount(); 
                var eventCategories = await GetPaginatedEventCategories(skip, take);

                var response = new PaginatedResponse<EventCategoryListDTO>
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

        private async Task<int> GetTotalEventCategoryCount()
        {
           
            return await _db.EventCategories.CountAsync();
        }

        private async Task<IEnumerable<EventCategoryListDTO>> GetPaginatedEventCategories(int skip, int take)
        {
            
            return await _db.EventCategories
                .Skip(skip)
                .Take(take)
                .Select(eventCategory => _mapper.Map<EventCategoryListDTO>(eventCategory))
                .ToListAsync();
        }


        public async Task<EventCategoryListDTO?> GetEventCategoryById(int id)
        {
            var eventCategory = await _db.EventCategories.FindAsync(id);

            if (eventCategory == null)
                return null;

            var eventCategoryDTO = _mapper.Map<EventCategoryListDTO>(eventCategory);

            return eventCategoryDTO;
        }

        public async Task<List<EventCategoryDTO>> UpdateEventCategory(int id, EventCategoryDTO request)
        {
            var existingEventCategory = await _db.EventCategories.FindAsync(id);

            if (existingEventCategory == null)
            {
                var eventCategory = _mapper.Map<EventCategory>(request);

                _db.EventCategories.Add(eventCategory);
                await _db.SaveChangesAsync();
            }
            _mapper.Map(request, existingEventCategory);

            await _db.SaveChangesAsync();

            
            var updatedEventCategories = await _db.EventCategories
                .Select(ec => _mapper.Map<EventCategoryDTO>(ec))
                .ToListAsync();

            return updatedEventCategories;
        }

        public async Task<List<EventCategoryListDTO>> GetAllEventCategory()
        {
            List<EventCategory> datalist = await _db.EventCategories.ToListAsync();
            return _mapper.Map<List<EventCategoryListDTO>>(datalist);
        }
    }
}
