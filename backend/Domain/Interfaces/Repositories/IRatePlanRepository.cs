using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRatePlanRepository
{
    RatePlan? GetByRoomCategoryId( Guid roomCategoryId );
    RatePlan? GetById( Guid id );
    void Save( RatePlan ratePlan );
    void Update( RatePlan ratePlan );
    IReadOnlyList<RatePlan> GetAll();
}