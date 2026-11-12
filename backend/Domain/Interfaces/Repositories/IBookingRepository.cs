using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IBookingRepository
{
    void Save( Booking booking );
    void Update( Booking booking );
    Booking? GetById( Guid id );
    IReadOnlyList<Booking> GetByUserId( Guid userId );
}