using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IClientRepository
{
    IReadOnlyList<Client> GetAll();
    Client? GetById( Guid id );
    void Save( Client client );
    void Update( Client client );
    void Delete( Guid id );
}