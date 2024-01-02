using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Mitra.Services.Interface;
using Mitra.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Services
{
    public static class DependencyResolverService
    {
        public static IServiceCollection ApplicationRegister(this IServiceCollection services)
        {
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IEventCategoryService, EventCategoryService>();
            services.AddTransient<IEventService, EventService>();
            return services;
        }
    }
}
