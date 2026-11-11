using Application.DTOs.User;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService( IUserRepository userRepository )
    {
        _userRepository = userRepository;
    }

    public UserDto GetUser( Guid id )
    {
        User user = _userRepository.FindUserById(id);
        if ( user == null )
        {
            throw new NotFoundException($"User with id: {id} not found");
        }
        
        return UserDto.ConvertUserToUserDto(user);
    }
}
