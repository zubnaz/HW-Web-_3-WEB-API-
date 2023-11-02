using BusinessLogic.ApiModels;
using BusinessLogic.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HW_Web__3_WEB_API_.Middlewares
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message,ex.Status);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode status = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                Status = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}
