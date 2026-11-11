using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RoomInBookingConfiguration : IEntityTypeConfiguration<RoomInBooking>
{
    public void Configure( EntityTypeBuilder<RoomInBooking> builder )
    {
        builder.ToTable( "room_in_booking" );
        builder.HasKey( rib => rib.Id );

        builder.HasQueryFilter( rib => rib.DeletedAt == null );

        builder.Property( rib => rib.BookingId )
               .IsRequired();

        builder.Property( rib => rib.RoomId )
               .IsRequired();

        builder.Property( rib => rib.ArrivalDate )
               .IsRequired();

        builder.Property( rib => rib.DepartureDate )
               .IsRequired();

        builder.Property( rib => rib.CreatedAt )
               .IsRequired();

        builder.Property( rib => rib.UpdatedAt );

        builder.Property( rib => rib.DeletedAt );

        builder.HasOne<Booking>()
               .WithMany()
               .HasForeignKey( rib => rib.BookingId )
               .OnDelete( DeleteBehavior.NoAction );

        builder.HasOne<Room>()
               .WithMany()
               .HasForeignKey( rib => rib.RoomId )
               .OnDelete( DeleteBehavior.NoAction );
    }
}