using MarkAPI.Bussines.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace MarkAPI.API.Middlewares
{
    public static class CustomExceptionHandlerMiddleware
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler(opt =>
            {
                opt.Run(async context =>
                {
                    var feature = context.Features.Get<IExceptionHandlerFeature>();
                    Exception exception = feature.Error;
                    if(exception is IBaseException ext)
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Message = ext.ExceptionMessage,
                            StatusCode = ext.StatusCode
                        });
                    }
                    else
                    {
                        await context.Response.WriteAsJsonAsync(new
                        {
                            Message = exception.Message,
                            StatusCode = 500
                        });
                    }

                });
            });
            return webApplication;
        }
    }
}
