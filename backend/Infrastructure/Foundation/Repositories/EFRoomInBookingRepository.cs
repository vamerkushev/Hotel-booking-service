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

    public IReadOnlyList<RoomInBooking> GetByReservationId( Guid reservationId )
    {
        return _dbContext.Set<RoomInBooking>()
            .Where( r => r.ReservationId == reservationId )
            .ToList();
    }

    public RoomInBooking? GetById( Guid id )
    {
        return _dbContext.Set<RoomInBooking>().Find( id );
    }

    public bool ExistsForReservationAndRoom( Guid reservationId, Guid roomId )
    {
        return _dbContext.Set<RoomInBooking>()
            .Any( r => r.ReservationId == reservationId && r.RoomId == roomId );
    }

    public int GetOverlappingCount( Guid roomId, DateOnly checkIn, DateOnly checkOut, Guid? excludeRoomInBookingId = null )
    {
        IQueryable<RoomInBooking> query = _dbContext.Set<RoomInBooking>()
            .Where( r => r.RoomId == roomId
                && r.CheckInDate < checkOut
                && r.CheckOutDate > checkIn );

        if ( excludeRoomInBookingId.HasValue )
        {
            query = query.Where( r => r.Id != excludeRoomInBookingId.Value );
        }

        return query.Count();
    }

    public void Save( RoomInBooking roomInBooking )
    {
        _dbContext.Set<RoomInBooking>().Add( roomInBooking );
        _dbContext.SaveChanges();
    }

    public void Update( RoomInBooking roomInBooking )
    {
        _dbContext.Update( roomInBooking );
        _dbContext.SaveChanges();
    }

    public void Delete( Guid id )
    {
        RoomInBooking? existing = GetById( id );
        if ( existing == null )
        {
            throw new KeyNotFoundException( $"RoomInBooking с {id} ID не найден!" );
        }
        _dbContext.Set<RoomInBooking>().Remove( existing );
        _dbContext.SaveChanges();
    }
}