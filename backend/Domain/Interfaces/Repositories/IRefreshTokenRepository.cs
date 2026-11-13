using Domain.Entities;

namespace Domain.Interfaces.Repositories;

public interface IRefreshTokenRepository
{
    RefreshToken? FindByTokenHash( string tokenHash );
    void Add( RefreshToken refreshToken );
    void Update( RefreshToken refreshToken );
    void RevokeAllForUser( Guid userId );
}
