using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFRoomRepository : IRoomRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFRoomRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public void Save( Room room )
    {
        _dbContext.Set<Room>().Add( room );
        _dbContext.SaveChanges();
    }

    public Room? GetById( Guid id ) => _dbContext.Set<Room>().Find( id );

    public IReadOnlyList<Room> GetAvailableRooms( Guid hotelId, DateOnly arrival, DateOnly departure )
    {
        return _dbContext.Set<Room>()
            .Where( r => r.HotelId == hotelId )
            .Where( r => !_dbContext.Set<RoomInBooking>().Any( rib =>
                rib.RoomId == r.Id &&
                rib.ArrivalDate < departure &&
                rib.DepartureDate > arrival ) )
            .ToList();
    }
}