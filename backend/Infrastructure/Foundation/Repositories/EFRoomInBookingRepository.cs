using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFRoomInBookingRepository : IRoomInBookingRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFRoomInBookingRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public void Save( RoomInBooking roomInBooking )
    {
        _dbContext.Set<RoomInBooking>().Add( roomInBooking );
        _dbContext.SaveChanges();
    }

    public IReadOnlyList<RoomInBooking> GetByBookingId( Guid bookingId )
    {
        IQueryable<RoomInBooking> query = _dbContext.Set<RoomInBooking>();
        List<RoomInBooking> roomsInBooking = query.Where( rib => rib.BookingId == bookingId ).ToList();

        return roomsInBooking;
    }
}