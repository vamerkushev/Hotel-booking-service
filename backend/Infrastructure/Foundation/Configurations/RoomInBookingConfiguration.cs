using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RoomInBookingConfiguration : IEntityTypeConfiguration<RoomInBooking>
{
    public void Configure( EntityTypeBuilder<RoomInBooking> builder )
    {
        builder.ToTable( "RoomInBookings" );
        builder.HasKey( r => r.Id );

        builder.Property( r => r.CheckInDate )
               .IsRequired();

        builder.Property( r => r.CheckOutDate )
               .IsRequired();

        builder.HasOne( r => r.Reservation )
               .WithMany( res => res.RoomInBookings )
               .HasForeignKey( r => r.ReservationId )
               .OnDelete( DeleteBehavior.Cascade );

        builder.HasOne( r => r.Room )
               .WithMany( room => room.RoomInBookings )
               .HasForeignKey( r => r.RoomId )
               .OnDelete( DeleteBehavior.Restrict );

        builder.HasIndex( r => new { r.ReservationId, r.RoomId } ).IsUnique();
    }
}