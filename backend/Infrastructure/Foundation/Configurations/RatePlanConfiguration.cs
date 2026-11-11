using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class RatePlanConfiguration : IEntityTypeConfiguration<RatePlan>
{
    public void Configure( EntityTypeBuilder<RatePlan> builder )
    {
        builder.ToTable( "rate_plan" );
        builder.HasKey( rp => rp.Id );

        builder.HasQueryFilter( rp => rp.DeletedAt == null );

        builder.Property( rp => rp.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( rp => rp.Description )
               .HasMaxLength( 500 )
               .IsRequired();

        builder.Property( rp => rp.Price )
               .HasPrecision( 10, 2 )
               .IsRequired();

        builder.Property( rp => rp.CreatedAt )
               .IsRequired();

        builder.Property( rp => rp.UpdatedAt );

        builder.Property( rp => rp.DeletedAt );
    }
}