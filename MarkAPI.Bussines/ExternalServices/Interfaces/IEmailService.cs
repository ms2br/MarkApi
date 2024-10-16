using MarkAPI.Bussines.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.ExternalServices.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmedAsync(UserDto user);
        Task SendForgotPasswordAsync(UserDto user,string token);
    }
}
