using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRatePlanRepository
{
    RatePlan? GetByRoomCategoryId( Guid roomCategoryId );
    void Save( RatePlan ratePlan );
}