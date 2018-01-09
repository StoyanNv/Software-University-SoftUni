namespace TeamBuilder.Data.Configuration
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TeamBuilder.Models;

    class InvitationConfig : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.HasKey(i=>i.Id);

            builder.HasOne(x => x.Team)
                .WithMany(i => i.Incitations)
                .HasForeignKey(x => x.TeamId)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasOne(x => x.InvitedUser)
                .WithMany(y => y.Invitations)
                .HasForeignKey(x => x.InvitedUserId)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
