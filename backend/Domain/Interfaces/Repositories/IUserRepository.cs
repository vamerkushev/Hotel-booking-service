using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    User? FindUserById( Guid id );
    User? FindUserByEmail( string email );
    void Add( User user );
    void Update( User user );
}
