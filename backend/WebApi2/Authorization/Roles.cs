using Domain.Enums;

namespace WebApi2.Authorization;

public static class Roles
{
    public const string Admin = nameof( UserRole.Admin );
    public const string Client = nameof( UserRole.Client );
}
