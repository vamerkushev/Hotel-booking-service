using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Infrastructure.Foundation.Repositories;

public class EFBookingRepository : IBookingRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFBookingRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public void Save( Booking booking )
    {
        _dbContext.Set<Booking>().Add( booking );
        _dbContext.SaveChanges();
    }

    public void Update( Booking booking )
    {
        _dbContext.Set<Booking>().Update( booking );
        _dbContext.SaveChanges();
    }

    public Booking? GetById( Guid id )
    {
        return _dbContext.Set<Booking>().Find( id );
    }

    public IReadOnlyList<Booking> GetByUserId( Guid userId ) => _dbContext.Set<Booking>().Where( b => b.UserId == userId ).ToList();
}