using MediatR;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SampleUserManagement.Application.Common.Behaviors
{
    public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationBehavior(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            bool useApiKey = _configuration.GetValue<bool>("Authentication:UseApiKey");
            if (useApiKey)
            {
                var isKeyExist = _httpContextAccessor.HttpContext?.Request.Headers.ContainsKey("x-api-key");
                if (!isKeyExist.HasValue || !isKeyExist.Value)
                {
                    throw new UnauthorizedAccessException("API Key is missing");
                }

                var extractedApiKey = _httpContextAccessor.HttpContext?.Request.Headers["x-api-key"];
                var apiKey = _configuration.GetValue<string>("Authentication:ApiKey");
                if (!extractedApiKey.HasValue || extractedApiKey.Value != apiKey)
                {
                    throw new Exception("Wrong API Key");
                }
            }

            return await next();
        }
    }
}
