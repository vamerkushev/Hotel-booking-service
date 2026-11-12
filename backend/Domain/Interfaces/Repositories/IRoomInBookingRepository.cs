using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRoomInBookingRepository
{
    void Save( RoomInBooking roomInBooking );
    IReadOnlyList<RoomInBooking> GetByBookingId( Guid bookingId );
    bool IsRoomAvailable( Guid roomId, DateOnly arrival, DateOnly departure );
}