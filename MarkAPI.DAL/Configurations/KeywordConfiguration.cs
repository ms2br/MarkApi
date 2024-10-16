using MarkAPI.CORE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkAPI.DAL.Configurations
{
    public class KeywordConfiguration :
        IEntityTypeConfiguration<Keyword>
    {
        public void Configure(EntityTypeBuilder<Keyword> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Word)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.NormalizedWord)
                .HasMaxLength(250)
                .IsRequired()
                .IsUnicode();

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Keywords)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
