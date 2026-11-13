using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFUserRepository : IUserRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFUserRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public User? FindUserById( Guid id )
    {
        return _dbContext.Set<User>().Find( id );
    }

    public User? FindUserByEmail( string email )
    {
        return _dbContext.Set<User>().FirstOrDefault( u => u.Email == email );
    }

    public void Add( User user )
    {
        _dbContext.Set<User>().Add( user );
        _dbContext.SaveChanges();
    }

    public void Update( User user )
    {
        _dbContext.Set<User>().Update( user );
        _dbContext.SaveChanges();
    }
}
