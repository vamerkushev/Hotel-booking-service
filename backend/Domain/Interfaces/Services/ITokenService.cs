using Domain.Entities;
using Domain.Interfaces.Models;

namespace Domain.Interfaces.Services;

public interface ITokenService
{
    AccessToken GenerateAccessToken( User user );
    string GenerateRefreshToken();
    string HashRefreshToken( string refreshToken );
    DateTime GetRefreshTokenExpiration();
}
