using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    User? FindUserById( Guid id );
    void Save( User user );
    void Update( User user );
    IReadOnlyList<User> GetAll();
    User? FindUserByEmail( string email );
}
