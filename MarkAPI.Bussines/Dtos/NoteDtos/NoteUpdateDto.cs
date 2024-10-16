using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.NoteDtos
{
    public class NoteUpdateDto
    {
        public string Content { get; set; }
        public IEnumerable<string> KeywordIds { get; set; }
    }

    public class NoteUpdateDtoValidator : 
        AbstractValidator<NoteUpdateDto>
    {
        public NoteUpdateDtoValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty();

            RuleFor(x => x.KeywordIds)
                .NotEmpty();
        }
    }
}
