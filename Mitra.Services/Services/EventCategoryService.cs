using AutoMapper;
using Mitra.Domain.Entity;
using Mitra.Domain;
using Mitra.Services.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mitra.Services.Interface;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

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

       


        public async Task<IEnumerable<EventCategoryDTO>> GetAllEventCategory()
        {
            List<EventCategory> eventCategories = await _db.EventCategories.ToListAsync();
            return _mapper.Map<List<EventCategoryDTO>>(eventCategories);
        }

        public async Task<EventCategoryDTO?> GetEventCategoryById(int id)
        {
            var eventCategory = await _db.EventCategories.FindAsync(id);

            if (eventCategory == null)
                return null;

            var eventCategoryDTO = _mapper.Map<EventCategoryDTO>(eventCategory);

            return eventCategoryDTO;
        }

        public async Task<List<EventCategoryDTO>> UpdateEventCategory(int id, EventCategoryDTO request)
        {
            var existingEventCategory = await _db.EventCategories.FindAsync(id);

            if (existingEventCategory == null)
            {
                // If the EventCategory with the given id doesn't exist, you may handle this scenario accordingly.
                // For simplicity, I'm throwing an exception here, but you can customize it based on your needs.
                return null;
            }

            // Assuming _mapper is an instance of AutoMapper configured to map EventCategoryDTO to EventCategory
            _mapper.Map(request, existingEventCategory);

            // Save changes to the database
            await _db.SaveChangesAsync();

            // Assuming you want to return a list of EventCategoryDTOs after the update
            var updatedEventCategories = await _db.EventCategories
                .Select(ec => _mapper.Map<EventCategoryDTO>(ec))
                .ToListAsync();

            return updatedEventCategories;
        }

    }
}
