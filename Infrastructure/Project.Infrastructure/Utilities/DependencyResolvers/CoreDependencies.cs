using Microsoft.Extensions.DependencyInjection;
using Project.Core.Utilities.Mapper;
using Project.Infrastructure.DAL;

namespace Project.Infrastructure.Utilities.DependencyResolvers
{
    public static class CoreDependencies
    {
        public static void AddCoreDependencies(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddAutoMapper(typeof(MapperProfile));
        }
    }
}
