using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RatePlanRoomCategoryConfiguration : IEntityTypeConfiguration<RatePlanRoomCategory>
{
    public void Configure( EntityTypeBuilder<RatePlanRoomCategory> builder )
    {
        builder.ToTable( "rate_plan_room_category" );
        builder.HasKey( rprc => rprc.Id );

        builder.Property( rprc => rprc.RatePlanId )
               .IsRequired();

        builder.Property( rprc => rprc.RoomCategoryId )
               .IsRequired();

        builder.HasOne<RatePlan>()
               .WithMany()
               .HasForeignKey( rprc => rprc.RatePlanId )
               .OnDelete( DeleteBehavior.NoAction );

        builder.HasOne<RoomCategory>()
               .WithMany()
               .HasForeignKey( rprc => rprc.RoomCategoryId )
               .OnDelete( DeleteBehavior.NoAction );
    }
}