using System.Net;
using Common;
using System.Text.Json;
using EntityCoreTemplate.Application.Helpers;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly TelegramBotHelper _telegramBotHelper;
        private readonly IHostEnvironment _environment;
        public ExceptionHandlingMiddleware(TelegramBotHelper telegramBotHelper, IHostEnvironment environment)
        {
            _telegramBotHelper = telegramBotHelper;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Request.EnableBuffering();
            try
            {
                await next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Unauthorized, ex, "Unauthorized access");
            }
            catch (DbUpdateException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex, "Database error");
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                var errorMessage = string.Join(", ", errors);
                await HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex, errorMessage);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception exception, string errorMessage = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            errorMessage = _environment.IsDevelopment() ? exception.Message : errorMessage ?? statusCode.ToString(); 
            var result = Result.Fail(errorMessage);
            var jsonResult = JsonSerializer.Serialize(result);
            await context.Response.WriteAsync(jsonResult);
            await _telegramBotHelper.SendExceptionAsync(exception);
        }
    }

    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}