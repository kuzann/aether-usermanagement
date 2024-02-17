using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SampleUserManagement.Application.Configurations
{
    public static class MapperSetup
    {
        public static void AddMapperSetup(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
