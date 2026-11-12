using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IHotelRepository
{
    void Save( Hotel hotel );
    void Update( Hotel hotel );
    IReadOnlyList<Hotel> GetAll();
    IReadOnlyList<Hotel> GetByCity( string city );
    Hotel? GetById( Guid id );
}