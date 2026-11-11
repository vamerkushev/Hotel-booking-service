using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure( EntityTypeBuilder<User> builder )
    {
        builder.ToTable( "User" );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.FirstName )
               .HasMaxLength( 15 )
               .IsRequired();
        
        builder.Property( r => r.SecondName )
            .HasMaxLength( 30 )
            .IsRequired();

        builder.Property( r => r.MiddleName )
            .HasMaxLength( 30 );

        builder.Property( r => r.Email )
            .HasMaxLength( 70 )
            .IsRequired();

        builder.Property( r => r.Phone )
            .HasMaxLength( 15 )
            .IsRequired();

        builder.Property( r => r.Password )
            .HasMaxLength( 70 )
            .IsRequired();

        builder.Property( r => r.Role )
            .IsRequired();

        builder.Property( r => r.CreatedAt )
            .IsRequired();

        builder.Property( r => r.DeletedAt );
    }
}