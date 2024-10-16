using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.Bussines.Dtos.NoteDtos
{
    public class NoteCreateDto
    {
        public string Content { get; set; }
        public IEnumerable<string> KeywordIds { get; set; }
    }

    public class NoteCreateDtoValidator : 
        AbstractValidator<NoteCreateDto>
    {
        public NoteCreateDtoValidator()
        {
            RuleFor(x => x.Content)                
                .NotEmpty();

            RuleFor(x => x.KeywordIds)
                .NotEmpty();
        }
    }
}
