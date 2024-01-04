using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Mitra.Services.Interface;
using Mitra.Services.Services;
using Swashbuckle.AspNetCore.Filters;
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

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IUserService, UserService>();


            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });


            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Add authorization services if needed
            services.AddAuthorization();


            return services;
        }


        //public static void AddEssentials(this IServiceCollection services)
        //{
        //    services.RegisterSwagger();
        //    //services.AddVersioning();

        //}

        //private static void RegisterSwagger(this IServiceCollection services)
        //{
        //    services.AddSwaggerGen(c =>
        //    {

        //        c.SwaggerDoc("v1", new OpenApiInfo
        //        {
        //            Version = "v1",
        //            Title = "RMS Engine",
        //            License = new OpenApiLicense()
        //            {

        //            }
        //        });
        //        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        //        {
        //            Name = "Authorization",
        //            In = ParameterLocation.Header,
        //            Type = SecuritySchemeType.ApiKey,
        //            Scheme = "Bearer",
        //            BearerFormat = "JWT",
        //            Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
        //        });
        //        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        //      {
        //      {
        //          new OpenApiSecurityScheme
        //          {
        //              Reference = new OpenApiReference
        //              {
        //                  Type = ReferenceType.SecurityScheme,
        //                  Id = "Bearer",
        //              },
        //              Scheme = "Bearer",
        //              Name = "Bearer",
        //              In = ParameterLocation.Header,
        //          }, new List<string>()
        //      },
        //      });
        //    });
        //}
    }

}
