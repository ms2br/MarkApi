
using MarkAPI.API.Middlewares;
using MarkAPI.Bussines.Dtos.Common;
using MarkAPI.Bussines.Dtos.RedisDtos;
using MarkAPI.Bussines.Dtos.TokenDtos;
using MarkAPI.CORE.Entities.Identity;
using MarkAPI.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace MarkAPI.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddCors(x =>
            {
                x.AddDefaultPolicy(opt => 
                opt.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod()
                );
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerBearer();
            builder.Services.AddDbContext<MarkDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            });
            builder.Services.AddApiLayer(builder.Configuration.GetSection("JwtToken").Get<JWT>());
            builder.Services.AddAuthentication();

            builder.Services.Configure<EmailDto>(builder.Configuration.GetSection("Email"));

            builder.Services.Configure<JWT>(builder.Configuration.GetSection("JwtToken"));

            builder.Services.Configure<RedisOption>(builder.Configuration.GetSection("Redis"));
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                });
                app.UseRedisContext();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCustomExceptionHandler();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseTokenCheck();
            app.MapControllers();

            app.Run();
        }
    }
}
