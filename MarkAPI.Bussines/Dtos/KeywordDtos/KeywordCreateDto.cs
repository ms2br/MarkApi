using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.KeywordDtos
{
    public class KeywordCreateDto
    {
        public string Word { get; set; }
    }


    public class KeywordCreateDtoValidator :
        AbstractValidator<KeywordCreateDto>
    {
        public KeywordCreateDtoValidator()
        {
            RuleFor(x => x.Word)
                .MaximumLength(250)
                .MinimumLength(2)
                .NotEmpty();
        }
    }
}
