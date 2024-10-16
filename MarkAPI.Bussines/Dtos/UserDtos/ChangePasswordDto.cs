using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.UserDtos
{
    public class ChangePasswordDto
    {
        public string CurrentPassword { get; set; }
        public string Passsword { get; set; }
    }

    public class ChangePasswordDtoValidator
        : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")
                .NotEmpty();

            RuleFor(x => x.Passsword)
                .NotEmpty().
                Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
        }
    }
}
