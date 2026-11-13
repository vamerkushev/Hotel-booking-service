using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure( EntityTypeBuilder<RefreshToken> builder )
    {
        builder.ToTable( "RefreshToken" );
        builder.HasKey( r => r.Id );

        builder.HasQueryFilter( r => r.User.DeletedAt == null );

        builder.Property( r => r.TokenHash )
            .HasMaxLength( 64 )
            .IsRequired();

        builder.HasIndex( r => r.TokenHash )
            .IsUnique();

        builder.Property( r => r.CreatedAt )
            .IsRequired();

        builder.Property( r => r.ExpiresAt )
            .IsRequired();

        builder.Property( r => r.RevokedAt );

        builder.HasOne( r => r.User )
            .WithMany()
            .HasForeignKey( r => r.UserId )
            .OnDelete( DeleteBehavior.Cascade );

        builder.Ignore( r => r.IsRevoked );
    }
}
