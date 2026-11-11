using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure( EntityTypeBuilder<Room> builder )
    {
        builder.ToTable( "room" );
        builder.HasKey( r => r.Id );

        builder.HasQueryFilter( r => r.DeletedAt == null );

        builder.Property( r => r.HotelId )
               .IsRequired();

        builder.Property( r => r.RoomCategoryId )
               .IsRequired();

        builder.Property( r => r.Number )
               .HasMaxLength( 20 )
               .IsRequired();

        builder.Property( r => r.CreatedAt )
               .IsRequired();

        builder.Property( r => r.UpdatedAt );

        builder.Property( r => r.DeletedAt );

        builder.HasOne<Hotel>()
               .WithMany()
               .HasForeignKey( r => r.HotelId )
               .OnDelete( DeleteBehavior.NoAction );

        builder.HasOne<RoomCategory>()
               .WithMany()
               .HasForeignKey( r => r.RoomCategoryId )
               .OnDelete( DeleteBehavior.NoAction );
    }
}