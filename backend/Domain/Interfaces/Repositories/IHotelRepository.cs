using Domain.Entities;
namespace Domain.Interfaces.Repositories;

public interface IHotelRepository
{
    void Save( Hotel hotel );
    IReadOnlyList<Hotel> GetByCity( string city );
}