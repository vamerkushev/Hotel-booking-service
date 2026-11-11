using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFRoomCategoryRepository : IRoomCategoryRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFRoomCategoryRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public void Save( RoomCategory roomCategory )
    {
        _dbContext.Set<RoomCategory>().Add( roomCategory );
        _dbContext.SaveChanges();
    }
}