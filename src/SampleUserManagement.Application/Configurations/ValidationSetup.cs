using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SampleUserManagement.Application.Configurations
{
    public static class ValidationSetup
    {
        public static void AddValidationSetup(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
