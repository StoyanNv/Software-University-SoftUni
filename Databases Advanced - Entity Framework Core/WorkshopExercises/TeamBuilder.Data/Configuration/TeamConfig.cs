using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamBuilder.Models;

namespace TeamBuilder.Data.Configuration
{
    class TeamConfig : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasAlternateKey(e => e.Name);

            builder.HasIndex(t=>t.Name).IsUnique();

            builder.Property(t=>t.Name)
                .HasMaxLength(25)
                .IsRequired();

            builder.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(32);

            builder.Property(t => t.Acronym)
                .HasColumnType("char(3)")
                .IsRequired();

            builder.HasOne(c => c.Creator)
                .WithMany(t => t.CreatedTeams)
                .HasForeignKey(c => c.CreatorId);
        }
    }
}
