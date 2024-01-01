using Microsoft.EntityFrameworkCore;
using Mitra.Domain;

namespace Mitra.API
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection"),
                b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
        }
    }
}
