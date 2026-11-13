using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;

namespace Infrastructure.Foundation.Seeding;

public class AdminSeeder
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public AdminSeeder( IUserRepository userRepository, IPasswordHasher passwordHasher )
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public void Seed( AdminSeedOptions options )
    {
        if ( string.IsNullOrWhiteSpace( options.Email ) || string.IsNullOrWhiteSpace( options.Password ) )
        {
            return;
        }

        string email = options.Email.Trim().ToLowerInvariant();
        if ( _userRepository.FindUserByEmail( email ) != null )
        {
            return;
        }

        User admin = new(
            "Admin",
            "Admin",
            null,
            email,
            "+70000000000",
            _passwordHasher.Hash( options.Password ),
            UserRole.Admin );

        _userRepository.Add( admin );
    }
}
