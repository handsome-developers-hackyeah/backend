using System.Text.Json;
using Comptee.Exceptions;
using Comptee.Middlewears.Models;

namespace Comptee.Middlewears;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;
    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(BaseAppException baseAppException)
        {
            await throwError(context, baseAppException.StatusCodeToRise, baseAppException.Errors);
        }
        catch(Exception exception)
        {
            await throwError(context, 500, new Dictionary<string, string[]> { { "Message", new string[] { exception.Message } } });
        }
    }

    private Task throwError(HttpContext context, int statusCode, Dictionary<string,string[]> errors)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = statusCode;
        return response.WriteAsync(JsonSerializer.Serialize(ApiResponse.Failure(statusCode,errors)));
    }
}