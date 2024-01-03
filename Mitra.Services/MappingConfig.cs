using AutoMapper;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;


namespace Mitra.Services
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ItemDTO, Item>().ReverseMap();
                config.CreateMap<Item, ItemDTO>().ReverseMap();
                config.CreateMap<EventCategory, EventCategoryDTO>().ReverseMap();
                config.CreateMap<EventCategoryDTO, EventCategory>().ReverseMap();
                config.CreateMap<Event,EventDTO>().ReverseMap();
                config.CreateMap<EventDTO,Event>().ReverseMap();
                config.CreateMap<User, UserDTO>().ReverseMap(); 
                config.CreateMap<UserDTO, User>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
