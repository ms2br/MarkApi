using Azure.Core;
using MarkAPI.Bussines.Dtos.Common;
using MarkAPI.Bussines.Dtos.UserDtos;
using MarkAPI.CORE.Entities.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace MarkAPI.Bussines.ExternalServices.Implements
{
    public class EmailService : IEmailService
    {

        UserManager<AppUser> _um { get; }
        IHttpContextAccessor _context { get; }
        IUrlHelperFactory _url { get; }
        IActionContextAccessor _actionContext { get; }
        IWebHostEnvironment _web { get; }
        IOptionsMonitor<EmailDto> _emailOption { get; }
        public EmailService(UserManager<AppUser> um,
            IConfiguration configuration,
             IUrlHelperFactory url,
             IActionContextAccessor actionContext,
             IHttpContextAccessor context,
             IWebHostEnvironment web,
             IOptionsMonitor<EmailDto> emailOption)
        {
            _um = um;
            _url = url;
            _actionContext = actionContext;
            _context = context;
            _web = web;
            _emailOption = emailOption;
        }

        public async Task SendEmailConfirmedAsync(UserDto user)
        {
            var token = await _um.GenerateEmailConfirmationTokenAsync(await _um.FindByEmailAsync(user.Email));
            var link = await CreateLinkAsync("Users", "EmailConfirmed", token,user);
            var template = await EditingEmailConfirmedTemplateAsync(user.UserName,link);
            await SendEmailAsync(template, "Email Confirm for Mark", user.Email);
        }

        public async Task SendForgotPasswordAsync(UserDto user,string token)
        {
            string link = await CreateLinkAsync("Auths", "ResetPassword", token, user);
            string template = await EditingEmailConfirmedTemplateAsync(user.UserName, link);

            await SendEmailAsync(template,"Forgot Password",user.Email);
        }

        async Task SendEmailAsync(string body,string subject,string mailAddress,bool isHtml = true)
        {
            EmailDto emailDto = _emailOption.CurrentValue;

            SmtpClient smtpClient = new SmtpClient(_emailOption.CurrentValue.Host, int.Parse(_emailOption.CurrentValue.Port));

            smtpClient.Credentials = new NetworkCredential(_emailOption.CurrentValue.UserName, _emailOption.CurrentValue.Password);
            smtpClient.EnableSsl = true;

            MailAddress from = new MailAddress(_emailOption.CurrentValue.UserName, "Mark");
            MailAddress to = new MailAddress(mailAddress);
            
            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = isHtml;
            smtpClient.Send(mailMessage);
        }

        async Task<string> CreateLinkAsync(string controller,string action,string token, UserDto user)
        {
            IUrlHelper url = _url.GetUrlHelper(_actionContext.ActionContext);

            string link = url.Action(action, controller, new
            {
                userId = user.Id,
                token = HttpUtility.UrlEncode(token)
            },_context.HttpContext.Request.Scheme);
            return link;
        }

        async Task<string> EditingEmailConfirmedTemplateAsync(string userName,string url)
        {
            using StreamReader sr = new StreamReader(Path.Combine(_web.WebRootPath, "schema/confirmEmail.html"));

            string template = await sr.ReadToEndAsync();
            template = template.Replace("[[[userName]]]",userName).Replace("[[[link]]]",url);
            return template;
        }
    }
}
