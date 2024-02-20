using AutoMapper;
using Mitra.Domain.Entity;
using Mitra.Services.Dtos;
using Mitra.Services.Dtos.Donation;
using Mitra.Services.Dtos.Donor;
using Mitra.Services.Dtos.Event;
using Mitra.Services.Dtos.EventCategory;
using Mitra.Services.Dtos.Expectation;
using Mitra.Services.Dtos.User;

namespace Mitra.Services
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {

                #region Event Category
                config.CreateMap<EventCategory, EventCategoryDTO>().ReverseMap();
                config.CreateMap<EventCategoryDTO, EventCategory>().ReverseMap();
                config.CreateMap<EventCategoryListDTO, EventCategory>().ReverseMap();
                config.CreateMap<EventCategory, EventCategoryListDTO>().ReverseMap();
                #endregion
                #region Item
                config.CreateMap<ItemDTO, Item>().ReverseMap();
                config.CreateMap<Item, ItemDTO>().ReverseMap();
                #endregion
                #region Event 
                config.CreateMap<Event,EventDTO>().ReverseMap();
                config.CreateMap<EventDTO,Event>().ReverseMap();
                config.CreateMap<Event, EventListDto>().ReverseMap();
                #endregion
                #region User
                config.CreateMap<User, UserDTO>().ReverseMap(); 
                config.CreateMap<UserDTO, User>().ReverseMap();
                config.CreateMap<User,LoginDto>().ReverseMap();
                config.CreateMap<LoginDto,User>().ReverseMap();
                #endregion
                #region Donor 
                config.CreateMap<Donor,DonorDto>().ReverseMap();
                config.CreateMap<DonorDto, Donor>().ReverseMap();
                config.CreateMap<Donor, DonorListDto>().ReverseMap()
                .ForMember(dest => dest.StreetId, opt => opt.MapFrom(src => src.StreetId.HasValue ? src.StreetId.Value : 0));
                #endregion
                #region Donation
                config.CreateMap<Donation, DonationDto>().ReverseMap();
                config.CreateMap<DonationDto, Donation>().ReverseMap();
                #endregion
                #region Expectation
                config.CreateMap<Expectation, ExpectationDto>().ReverseMap();
                config.CreateMap<ExpectationDto, Expectation>().ReverseMap();
                config.CreateMap<Expectation, ExpectationAddDto>().ReverseMap();
                config.CreateMap<ExpectationAddDto, Expectation>().ReverseMap();
                #endregion
                
            });

            return mappingConfig;
        }
    }
}
