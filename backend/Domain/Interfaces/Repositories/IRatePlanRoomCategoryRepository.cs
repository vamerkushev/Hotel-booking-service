using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRatePlanRoomCategoryRepository
{
    void Save( RatePlanRoomCategory ratePlanRoomCategory );
    RatePlanRoomCategory? GetById( Guid id );
    void Delete( RatePlanRoomCategory ratePlanRoomCategory );
}