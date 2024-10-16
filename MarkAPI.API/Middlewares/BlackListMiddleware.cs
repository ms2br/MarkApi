using MarkAPI.Bussines.Exceptions.Common;
using MarkAPI.Bussines.Exceptions;
using MarkAPI.Bussines.ExternalServices.Interfaces;
using MarkAPI.CORE.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using MarkAPI.Bussines.Helpers;

namespace MarkAPI.API.Middlewares
{
    public static class BlackListMiddleware
    {
        public static IApplicationBuilder UseTokenCheck(this
            WebApplication app)
        {
            app.Use(async (context, next) =>
            {
                using (var scope = context.RequestServices.CreateScope())
                {
                    var servic = context.RequestServices
                    .GetRequiredService<IBlackListService>();
                    if (await servic.TokenCheckAsync(context.GetUserToken()))
                        throw new NotFoundException<AppUser>(ExceptionMessages.UserNotFoundMessage);
                }
                await next();
            });
            return app;
        }
    }
}
