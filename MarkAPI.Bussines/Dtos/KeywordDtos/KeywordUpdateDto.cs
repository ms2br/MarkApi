using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.KeywordDtos
{
    public class KeywordUpdateDto
    {
        public string Word { get; set; }
    }

    public class KeywordUpdateDtoValidator : 
        AbstractValidator<KeywordUpdateDto>
    {
        public KeywordUpdateDtoValidator()
        {
            RuleFor(x => x.Word)
                .MaximumLength(250)
                .MinimumLength(2)
                .NotEmpty();
        }
    }
}
