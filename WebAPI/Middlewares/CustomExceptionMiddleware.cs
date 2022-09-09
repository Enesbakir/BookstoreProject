using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebAPI.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next){
            _next = next;
        }

        public async Task Invoke(HttpContext context){
            var watch =Stopwatch.StartNew();
            try
            {
                string message = "[Request] HTTP "+context.Request.Method+ " - "+context.Request.Path;

                await _next(context); //***Bu kısmın ne iş yaptığına bak!!!!***
                
                message = "[Response] HTTP "+context.Request.Method+ " - "+context.Request.Path;
            }
            catch (Exception ex)
            {
                watch.Stop();
                await ExceptionHandler(context,ex);
            }     
        }
        private Task ExceptionHandler(HttpContext context, Exception ex)
        {
            string message ="[Error] HTTP "+context.Request.Method+" - "+ context.Response.StatusCode+ " error message " + ex.Message;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "/application/json";
            var result = JsonConvert.SerializeObject(new {ERROR=ex.Message}, Formatting.None);
            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}