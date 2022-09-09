using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebAPI.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next){
            _next = next;
        }

        public async Task Invoke(HttpContext context){
            var watch =Stopwatch.StartNew();
            if(context.Request.HttpContext.Session.GetInt32("UserId") is null){
                throw new InvalidOperationException("Token and Authorization Exception");
            }
            else{
                await _next(context);
            }
           
            // try
            // {
            //     string message = "[Request] HTTP "+context.Request.Method+ " - "+context.Request.Path;

            //     await _next(context); //***Bu kısmın ne iş yaptığına bak!!!!***
                
            //     message = "[Response] HTTP "+context.Request.Method+ " - "+context.Request.Path;
            // }
            // catch (Exception ex)
            // {
            //     watch.Stop();
            //     await ExceptionHandler(context,ex);
            // }     
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

    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}