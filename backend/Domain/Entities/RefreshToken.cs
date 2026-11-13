namespace Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public string TokenHash { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime ExpiresAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }

    private RefreshToken()
    {
    }

    public RefreshToken( Guid userId, string tokenHash, DateTime expiresAt )
    {
        UserId = userId;
        TokenHash = tokenHash;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;
    }

    public bool IsRevoked => RevokedAt != null;

    public bool IsActive( DateTime now )
    {
        return !IsRevoked && ExpiresAt > now;
    }

    public void Revoke()
    {
        RevokedAt ??= DateTime.UtcNow;
    }
}
