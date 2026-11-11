using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure( EntityTypeBuilder<Booking> builder )
    {
        builder.ToTable( "booking" );
        builder.HasKey( b => b.Id );

        builder.HasQueryFilter( b => b.DeletedAt == null );

        builder.Property( b => b.UserId )
                .IsRequired();

        builder.Property( b => b.Total )
               .HasPrecision( 10, 2 )
               .IsRequired();

        builder.Property( b => b.Currency )
               .HasConversion<string>()
               .HasMaxLength( 3 )
               .IsRequired();

        builder.Property( b => b.UserCount )
               .IsRequired();

        builder.Property( b => b.BookingDate )
               .IsRequired();

        builder.Property( b => b.CreatedAt )
               .IsRequired();

        builder.Property( b => b.UpdatedAt );

        builder.Property( b => b.DeletedAt );

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey( b => b.UserId )
               .OnDelete( DeleteBehavior.NoAction );
    }
}