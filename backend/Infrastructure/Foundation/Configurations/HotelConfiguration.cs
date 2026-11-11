using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
    public void Configure( EntityTypeBuilder<Hotel> builder )
    {
        builder.ToTable( "hotel" );
        builder.HasKey( h => h.Id );

        builder.HasQueryFilter( h => h.DeletedAt == null );

        builder.Property( h => h.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( h => h.Country )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( h => h.City )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( h => h.Street )
               .HasMaxLength( 200 )
               .IsRequired();

        builder.Property( h => h.Stars )
               .IsRequired();

        builder.Property( h => h.CreatedAt )
               .IsRequired();

        builder.Property( h => h.UpdatedAt );

        builder.Property( h => h.DeletedAt );
    }
}