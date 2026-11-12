using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFRatePlanRepository : IRatePlanRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFRatePlanRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public void Save( RatePlan ratePlan )
    {
        _dbContext.Set<RatePlan>().Add( ratePlan );
        _dbContext.SaveChanges();
    }

    public void Update( RatePlan ratePlan )
    {
        _dbContext.Set<RatePlan>().Update( ratePlan );
        _dbContext.SaveChanges();
    }

    public IReadOnlyList<RatePlan> GetAll() => _dbContext.Set<RatePlan>().ToList();

    public RatePlan? GetById( Guid id ) => _dbContext.Set<RatePlan>().Find( id );

    public RatePlan? GetByRoomCategoryId( Guid roomCategoryId )
    {
        var query = from rprc in _dbContext.Set<RatePlanRoomCategory>()
                    join rp in _dbContext.Set<RatePlan>() on rprc.RatePlanId equals rp.Id
                    where rprc.RoomCategoryId == roomCategoryId
                    select rp;

        return query.FirstOrDefault();
    }
}