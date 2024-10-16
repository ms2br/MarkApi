using MarkAPI.CORE.Entities.Identity;
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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.NormalizedUserName)
                .HasMaxLength(32);

            builder.Property(x => x.UserName)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(x => x.NormalizedEmail)
                .HasMaxLength(255);

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(x => x.ImgUrl)
                .IsRequired(false);
        }
    }
}
