using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using SampleUserManagement.Application.Common.Behaviors;
using SampleUserManagement.Application.Common.Exceptions;
using System.Reflection;

namespace SampleUserManagement.Application.Configurations
{
    public static class MediatRSetup
    {
        public static void AddMediatRSetup(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(GlobalExceptionHandler<,,>));
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
                config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            });
        }
    }
}
