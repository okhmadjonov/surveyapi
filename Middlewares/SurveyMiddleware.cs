using surveyapi.Exceptions;
using surveyapi.Models;

namespace surveyapi.Middlewares;

public class SurveyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<SurveyMiddleware> _logger;

    public SurveyMiddleware(RequestDelegate next, ILogger<SurveyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (SurveyException ex)
        {
            await HandleException(context, ex.Code, ex.Message, ex.Global);
        }
        catch (Exception ex)
        {
            //Log
            _logger.LogError(ex.ToString());

            await HandleException(context, 500, "", true);
        }
    }

    public async Task HandleException(HttpContext context, int code, string message, bool? Global)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(
            new ResponseModel<string>
            {
                Status = false,
                Error = message,
                Data = null,
                GlobalError = Global
            }
        );
    }
}
