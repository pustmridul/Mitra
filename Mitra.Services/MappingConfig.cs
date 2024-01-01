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
            });

            return mappingConfig;
        }
    }
}
