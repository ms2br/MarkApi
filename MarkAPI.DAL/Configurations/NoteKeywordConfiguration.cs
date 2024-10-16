using MarkAPI.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.DAL.Configurations
{
    public class NoteKeywordConfiguration :
        IEntityTypeConfiguration<NoteKeyword>
    {
        public void Configure(EntityTypeBuilder<NoteKeyword> builder)
        {
            builder.Ignore(x => x.Id)
                .Ignore(x => x.IsDeleted)
                .Ignore(x=> x.UserId);

            builder.HasKey(x => new { x.KeywordId, x.NoteId });

            builder.HasOne(x => x.Note)
                .WithMany(x => x.Keywords)
                .HasForeignKey(x => x.NoteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Keyword)
                .WithMany(x => x.Notes)
                .HasForeignKey(x => x.KeywordId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
