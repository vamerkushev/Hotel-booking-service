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

    public void Update( RoomCategory roomCategory )
    {
        _dbContext.Set<RoomCategory>().Update( roomCategory );
        _dbContext.SaveChanges();
    }

    public RoomCategory? GetById( Guid id ) => _dbContext.Set<RoomCategory>().Find( id );
    public IReadOnlyList<RoomCategory> GetAll() => _dbContext.Set<RoomCategory>().ToList();
}