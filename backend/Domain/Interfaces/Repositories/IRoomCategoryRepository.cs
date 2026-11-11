using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRoomCategoryRepository
{
    void Save( RoomCategory roomCategory );
}