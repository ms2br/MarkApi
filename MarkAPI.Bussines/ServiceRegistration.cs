using FluentValidation.AspNetCore;
using MarkAPI.Bussines.Dtos.UserDtos;
using MarkAPI.Bussines.Repositories.Implements;
using MarkAPI.Bussines.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<INoteService, NoteService>();
            services.AddScoped<IKeywordService, KeywordService>();
            services.AddScoped<IBlackListService, BlackListService>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IRedisContext, RedisContext>();
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<IKeywordRepository, KeywordRepository>();
            return services;
        }


        public static IServiceCollection AddBussinesLayer(this IServiceCollection services)
        {
            services.AddServices();
            services.AddRepositories();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<RegisterDtoValidator>());
            return services;
        }
    }
}
