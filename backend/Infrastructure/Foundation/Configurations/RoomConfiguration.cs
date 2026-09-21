using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure( EntityTypeBuilder<Room> builder )
    {
        builder.ToTable( "Rooms" );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.Number )
               .HasMaxLength( 20 )
               .IsRequired();

        builder.Property( r => r.Price )
               .HasPrecision( 10, 2 )
               .IsRequired();

        builder.HasOne( r => r.Property )
               .WithMany()
               .HasForeignKey( r => r.PropertyId )
               .OnDelete( DeleteBehavior.Restrict );

        builder.HasOne( r => r.RoomType )
               .WithMany()
               .HasForeignKey( r => r.RoomTypeId )
               .OnDelete( DeleteBehavior.Restrict );

        builder.HasIndex( r => new { r.PropertyId, r.Number } ).IsUnique();
    }
}