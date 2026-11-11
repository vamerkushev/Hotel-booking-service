using Domain.Enums;

namespace Application.DTOs.User;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string? MiddleName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    
    public static UserDto ConvertUserToUserDto( Domain.Entities.User user )
    {
        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            SecondName = user.SecondName,
            MiddleName = user.MiddleName,
            Email = user.Email,
            Phone = user.Phone,
        };
    }
}
