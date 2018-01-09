using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasAlternateKey(e => e.Username);

            builder.Property(u => u.Username).IsRequired();

            builder.Property(u => u.Password).IsRequired();

            builder
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Property(u => u.FirstName)
                .HasMaxLength(25);

            builder.Property(u => u.Password)
                .HasMaxLength(30);

        }
    }
}
