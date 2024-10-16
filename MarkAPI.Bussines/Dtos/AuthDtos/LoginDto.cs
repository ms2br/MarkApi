using FluentValidation;
using MarkAPI.Bussines.Exceptions.AuthExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.AuthDtos
{
    public class LoginDto
    {
        public string NameOrEmail { get; set; }
        public string Password { get; set; }
    }

    public class AuthDtoValidator : AbstractValidator<LoginDto>
    {
        public AuthDtoValidator()
        {
            RuleFor(x => x.NameOrEmail)
                .NotEmpty();

            RuleFor(x => x.Password)
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .NotEmpty()
                .WithMessage(ExceptionMessages.UserOrPassswordWrongMessage);
        }
    }
}
