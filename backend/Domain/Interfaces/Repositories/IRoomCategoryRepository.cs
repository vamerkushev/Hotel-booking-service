using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRoomCategoryRepository
{
    void Save( RoomCategory roomCategory );
    void Update( RoomCategory roomCategory );
    RoomCategory? GetById( Guid id );
    IReadOnlyList<RoomCategory> GetAll();
}