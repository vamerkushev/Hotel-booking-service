using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFHotelRepository : IHotelRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFHotelRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public void Save( Hotel hotel )
    {
        _dbContext.Set<Hotel>().Add( hotel );
        _dbContext.SaveChanges();
    }

    public IReadOnlyList<Hotel> GetByCity( string city )
        => _dbContext.Set<Hotel>().Where( h => h.City.Contains( city ) ).ToList();
}