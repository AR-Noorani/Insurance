using App.RestApi.ApiResponse;
using App.RestApi.Utilities;
using DomainModels.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.RestApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var myApplicationException = ex as MyApplicationException;
                var message = myApplicationException?.Message ?? MyApplicationException.BASE_MESSAGE;
                var traceId = myApplicationException?.TraceId ?? TraceIdGenerator.Generate();

                logger.LogError(ex, message, traceId);

                var result = GenerateErrorJson(message, traceId);

                context.Response.StatusCode = (int)StatusCodeFinder.FindHttpStatusCode(ex);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(result);
            }
        }

        private static string GenerateErrorJson(string message, string traceId)
        {
            var apiError = new ApiError(message, traceId);
            var apiResult = ApiResult.From(apiError);

            JsonSerializerOptions jsonSerializerOptions = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };
            var result = JsonSerializer.Serialize(apiResult, jsonSerializerOptions);
            return result;
        }
    }
}
