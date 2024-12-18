using EquipLease.Application.Common;
using EquipLease.Application.Configurations;
using EquipLease.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace EquipLease.WebApi.Authentication;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly AuthenticationConfiguration _authConfig;
    private readonly ILogger<ApiKeyAuthFilter> _logger;

    public ApiKeyAuthFilter(IOptions<AuthenticationConfiguration> authConfig, ILogger<ApiKeyAuthFilter> logger)
    {
        _logger = logger;
        _authConfig = authConfig.Value;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Set the API Key in an environment variable "Authentication__ApiKey"
        if (!context.HttpContext.Request.Headers.TryGetValue(_authConfig.HeaderName,
                out var extractedApiKey))
        {
            _logger.LogWarning("Missing API Key in request.");

            context.Result = new UnauthorizedObjectResult(
                new Result<None> { ErrorCode = ErrorCode.ApiKeyMissing });

            return;
        }

        if (_authConfig.ApiKey.Equals(extractedApiKey)) return;

        _logger.LogWarning("Invalid API Key provided.");

        context.Result = new UnauthorizedObjectResult(
            new Result<None> { ErrorCode = ErrorCode.InvalidApiKey });
    }
}