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
                config.CreateMap<EventDTO,Event>().
                ReverseMap();
                config.CreateMap<Event, EventListDto>().ReverseMap();
                config.CreateMap<User, UserDTO>().ReverseMap(); 
                config.CreateMap<UserDTO, User>().ReverseMap();
                config.CreateMap<Donor,DonorDto>().ReverseMap();
                config.CreateMap<DonorDto, Donor>().ReverseMap();
                config.CreateMap<Donation, DonationDto>().ReverseMap();
                config.CreateMap<DonationDto, Donation>().ReverseMap();
                config.CreateMap<Expectation, ExpectationDto>().ReverseMap();
                config.CreateMap<ExpectationDto, Expectation>().ReverseMap();
                config.CreateMap<EventCategoryListDTO, EventCategory>().ReverseMap();
                config.CreateMap< EventCategory, EventCategoryListDTO>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}
