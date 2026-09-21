using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFRoomRepository : IRoomRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFRoomRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Room> GetAll()
    {
        return _dbContext.Set<Room>().ToList();
    }

    public IReadOnlyList<Room> GetByPropertyId( Guid propertyId )
    {
        return _dbContext.Set<Room>().Where( r => r.PropertyId == propertyId ).ToList();
    }

    public Room? GetById( Guid id )
    {
        return _dbContext.Set<Room>().Find( id );
    }

    public void Save( Room room )
    {
        _dbContext.Set<Room>().Add( room );
        _dbContext.SaveChanges();
    }

    public void Update( Room room )
    {
        _dbContext.Update( room );
        _dbContext.SaveChanges();
    }

    public void Delete( Guid id )
    {
        Room? existing = GetById( id );
        if ( existing == null )
        {
            throw new KeyNotFoundException( $"Room с {id} ID не найден!" );
        }
        _dbContext.Set<Room>().Remove( existing );
        _dbContext.SaveChanges();
    }
}