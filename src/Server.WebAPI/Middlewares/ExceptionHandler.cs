using FluentValidation;
using Newtonsoft.Json;
using Server.Application.Common;

namespace Server.WebAPI.Middlewares;

public sealed class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = ex.Errors.Select(p => p.ErrorMessage).ToList();
            var result = new Result<object>(null, errors, null);
            var resultJson = JsonConvert.SerializeObject(result);
            await context.Response.WriteAsync(resultJson);
        }
    }
}
