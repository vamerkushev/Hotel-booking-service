using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFRatePlanRoomCategoryRepository : IRatePlanRoomCategoryRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFRatePlanRoomCategoryRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public void Save( RatePlanRoomCategory ratePlanRoomCategory )
    {
        _dbContext.Set<RatePlanRoomCategory>().Add( ratePlanRoomCategory );
        _dbContext.SaveChanges();
    }

    public RatePlanRoomCategory? GetById( Guid id ) => _dbContext.Set<RatePlanRoomCategory>().Find( id );

    public void Delete( RatePlanRoomCategory ratePlanRoomCategory )
    {
        _dbContext.Set<RatePlanRoomCategory>().Remove( ratePlanRoomCategory );
        _dbContext.SaveChanges();
    }
}