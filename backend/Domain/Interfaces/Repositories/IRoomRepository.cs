using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRoomRepository
{
    void Save( Room room );
    void Update( Room room );
    IReadOnlyList<Room> GetAll();
    Room? GetById( Guid id );
    IReadOnlyList<Room> GetAvailableRooms( Guid hotelId, DateOnly arrival, DateOnly departure );
}