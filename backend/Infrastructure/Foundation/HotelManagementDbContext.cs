using Infrastructure.Foundation.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation;

public class HotelManagementDbContext : DbContext
{
    public HotelManagementDbContext( DbContextOptions<HotelManagementDbContext> options )
            : base( options )
    {
    }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        base.OnModelCreating( modelBuilder );

        modelBuilder.ApplyConfiguration( new HotelConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomCategoryConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomConfiguration() );
        modelBuilder.ApplyConfiguration( new BookingConfiguration() );
        modelBuilder.ApplyConfiguration( new RoomInBookingConfiguration() );
        modelBuilder.ApplyConfiguration( new RatePlanConfiguration() );
        modelBuilder.ApplyConfiguration( new RatePlanRoomCategoryConfiguration() );
        modelBuilder.ApplyConfiguration( new UserConfiguration() );
    }
}