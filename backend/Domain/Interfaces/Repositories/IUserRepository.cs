using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IUserRepository
{
    User? FindUserById( Guid id );
    void Save( User user );
}
