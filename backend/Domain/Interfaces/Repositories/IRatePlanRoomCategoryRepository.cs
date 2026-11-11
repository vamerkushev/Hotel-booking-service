using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IRatePlanRoomCategoryRepository
{
    void Save( RatePlanRoomCategory ratePlanRoomCategory );
}