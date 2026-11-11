using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRoomRepository
{
    void Save( Room room );
    Room? GetById( Guid id );
    IReadOnlyList<Room> GetAvailableRooms( Guid hotelId, DateOnly arrival, DateOnly departure );
}