using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetProjectDraft.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetProjectDraft.Infrastructure.Configurations.Write
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.ComplexProperty(u => u.Email,
                emailBuilder => { emailBuilder.Property(e => e.Value).HasColumnName("email"); });

            builder.ComplexProperty(u => u.Role, roleBuilder =>
            {
                roleBuilder.Property(r => r.Name).HasColumnName("role");
                roleBuilder.Property(r => r.Permissions).HasColumnName("permissions");
            });

            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.TelegramId).IsRequired(false);
        }
    }
}
