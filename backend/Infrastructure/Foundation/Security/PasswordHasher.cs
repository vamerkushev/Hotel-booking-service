using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Foundation.Security;

public class PasswordHasher : IPasswordHasher
{
    private static readonly object NoUser = new();
    private readonly PasswordHasher<object> _hasher = new();

    public string Hash( string password )
    {
        return _hasher.HashPassword( NoUser, password );
    }

    public bool Verify( string passwordHash, string password )
    {
        return _hasher.VerifyHashedPassword( NoUser, passwordHash, password ) != PasswordVerificationResult.Failed;
    }
}
