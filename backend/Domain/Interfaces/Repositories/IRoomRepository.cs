using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRoomRepository
{
    IReadOnlyList<Room> GetAll();
    IReadOnlyList<Room> GetByPropertyId( Guid propertyId );
    Room? GetById( Guid id );
    void Save( Room room );
    void Update( Room room );
    void Delete( Guid id );
}