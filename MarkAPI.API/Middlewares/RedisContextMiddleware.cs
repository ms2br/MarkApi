using MarkAPI.Bussines.Dtos.RedisDtos;
using MarkAPI.Bussines.ExternalContext.Interfaces;
using Microsoft.Extensions.Options;
using static System.Net.Mime.MediaTypeNames;

namespace MarkAPI.API.Middlewares
{
    public static class RedisContextMiddleware
    {
        static IRedisContext redisContext { get; set; }

        public static IApplicationBuilder UseRedisContext(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                using (IServiceScope scope = context.RequestServices.CreateScope())
                {
                    redisContext = scope.ServiceProvider.
                    GetRequiredService<IRedisContext>();
                    IOptionsMonitor<RedisOption>? options = scope.ServiceProvider.
                    GetRequiredService<IOptionsMonitor<RedisOption>>();
                    if (redisContext.Database is null)
                        OnOptionsChanged(options.CurrentValue);
                    else
                        options.OnChange(OnOptionsChanged);
                }
                await next();
            });
            return app;
        }

        public static void OnOptionsChanged(RedisOption option)
        {
            redisContext.GetDatabaseAsync(option).Wait();
        }
    }
}
