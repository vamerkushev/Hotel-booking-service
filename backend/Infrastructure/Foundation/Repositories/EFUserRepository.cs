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
}
