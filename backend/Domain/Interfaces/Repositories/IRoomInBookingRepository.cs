using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomInBookingRepository
{
    IReadOnlyList<RoomInBooking> GetByReservationId( Guid reservationId );
    RoomInBooking? GetById( Guid id );
    bool ExistsForReservationAndRoom( Guid reservationId, Guid roomId );
    int GetOverlappingCount( Guid roomId, DateOnly checkIn, DateOnly checkOut, Guid? excludeRoomInBookingId = null );
    void Save( RoomInBooking roomInBooking );
    void Update( RoomInBooking roomInBooking );
    void Delete( Guid id );
}