using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Foundation.Repositories;

public class EFRefreshTokenRepository : IRefreshTokenRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFRefreshTokenRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public RefreshToken? FindByTokenHash( string tokenHash )
    {
        return _dbContext.Set<RefreshToken>()
            .Include( r => r.User )
            .FirstOrDefault( r => r.TokenHash == tokenHash );
    }

    public void Add( RefreshToken refreshToken )
    {
        _dbContext.Set<RefreshToken>().Add( refreshToken );
        _dbContext.SaveChanges();
    }

    public void Update( RefreshToken refreshToken )
    {
        _dbContext.Set<RefreshToken>().Update( refreshToken );
        _dbContext.SaveChanges();
    }

    public void RevokeAllForUser( Guid userId )
    {
        DateTime now = DateTime.UtcNow;

        _dbContext.Set<RefreshToken>()
            .IgnoreQueryFilters()
            .Where( r => r.UserId == userId && r.RevokedAt == null )
            .ExecuteUpdate( setters => setters.SetProperty( r => r.RevokedAt, now ) );
    }
}
