using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFClientRepository : IClientRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFClientRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<Client> GetAll()
    {
        return _dbContext.Set<Client>().ToList();
    }

    public Client? GetById( Guid id )
    {
        return _dbContext.Set<Client>().Find( id );
    }

    public void Save( Client client )
    {
        _dbContext.Set<Client>().Add( client );
        _dbContext.SaveChanges();
    }

    public void Update( Client client )
    {
        _dbContext.Update( client );
        _dbContext.SaveChanges();
    }

    public void Delete( Guid id )
    {
        Client? existing = GetById( id );
        if ( existing == null )
        {
            throw new KeyNotFoundException( $"Client с {id} ID не найден!" );
        }
        _dbContext.Set<Client>().Remove( existing );
        _dbContext.SaveChanges();
    }
}