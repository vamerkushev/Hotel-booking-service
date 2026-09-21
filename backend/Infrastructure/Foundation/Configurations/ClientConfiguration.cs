using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Foundation.Configurations;

internal class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure( EntityTypeBuilder<Client> builder )
    {
        builder.ToTable( "Clients" );
        builder.HasKey( c => c.Id );

        builder.Property( c => c.Name )
               .HasMaxLength( 100 )
               .IsRequired();

        builder.Property( c => c.Phone )
               .HasMaxLength( 20 )
               .IsRequired();
    }
}