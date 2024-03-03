using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
            services.AddTransient<IDonorService, DonorService>();
            services.AddTransient<IDonationService, DonationService>();
            services.AddTransient<IExpectationService, ExpectationService>();
            //services.AddCors();

            // Add CORS policy to allow requests from any origin
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            

            services.AddControllers();

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

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"])),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });
            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme ;
                //options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                //----
                //options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme; // Use Google as the default authentication challenge scheme
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                })
                //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("cookie", options =>
                {
                    //...

                    // `SameSite=Strict` only works for SPA apps where
                    // initial "catch-all" `index.html` page is not secured.
                    options.Cookie.SameSite = SameSiteMode.Lax;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "826276899262-vn0q0vi9f5of7jhtsau3bjm96m2ttbmv.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                    options.CallbackPath = "/signin-google";
                });
            services.AddControllersWithViews();



            services.AddAuthentication();

            services.AddAuthorization();


            return services;
        }


    }

}
