using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RoomCategoryConfiguration : IEntityTypeConfiguration<RoomCategory>
{
    public void Configure( EntityTypeBuilder<RoomCategory> builder )
    {
        builder.ToTable( "room_category" );
        builder.HasKey( rc => rc.Id );

        builder.HasQueryFilter( rc => rc.DeletedAt == null );

        builder.Property( rc => rc.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( rc => rc.MinUserCount )
               .IsRequired();

        builder.Property( rc => rc.MaxUserCount )
               .IsRequired();

        builder.Property( rc => rc.CreatedAt )
               .IsRequired();

        builder.Property( rc => rc.UpdatedAt );

        builder.Property( rc => rc.DeletedAt );
    }
}