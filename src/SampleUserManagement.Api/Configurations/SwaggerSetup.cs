using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System;
using Swashbuckle.AspNetCore.Filters;

namespace SampleUserManagement.Api.Configurations
{
	public static class SwaggerSetup
	{
		public static void AddSwaggerSetup(this IServiceCollection services)
		{
            //services.AddSwaggerGen();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "UserManagement.API",
                    Version = "v1",
                    Description = "Sample User Management API",
                    Contact = new OpenApiContact
                    {
                        Name = "Ryan Gifari",
                        Url = new Uri("https://github.com/kuzann")
                    }
                });
                options.DescribeAllParametersInCamelCase();
                options.OrderActionsBy(descriptor => descriptor.RelativePath);

                var xmlfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlfile);
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                // To Enable authorization using Swagger (JWT)    
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });

                // Maps all structured ids to the guid type to show correctly on swagger
                //var allGuids = typeof(IGuid).Assembly.GetTypes().Where(type => typeof(IGuid).IsAssignableFrom(type) && !type.IsInterface)
                //    .ToList();
                //foreach (var guid in allGuids)
                //{
                //    options.MapType(guid, () => new OpenApiSchema { Type = "string", Format = "uuid" });
                //}
            });
        }

        public static IApplicationBuilder UseSwaggerSetup(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.RoutePrefix = "api-docs";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.DocExpansion(DocExpansion.List);
                options.DisplayRequestDuration();
            });
            return app;
        }
    }
}
