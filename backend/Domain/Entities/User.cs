using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string SecondName { get; private set; } = string.Empty;
    public string MiddleName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public UserRole Role { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

    private User()
    {
    }

    public User(
        string firstName,
        string secondName,
        string middleName,
        string email,
        string phone,
        string password,
        UserRole role = UserRole.Client )
    {
        FirstName = firstName;
        SecondName = secondName;
        MiddleName = middleName;
        Email = email;
        Phone = phone;
        Password = password;
        Role = role;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(
        string firstName,
        string secondName,
        string middleName,
        string email,
        string phone,
        UserRole role )
    {
        FirstName = firstName;
        SecondName = secondName;
        MiddleName = middleName;
        Email = email;
        Phone = phone;
        Role = role;
    }

    public void ChangePassword( string password )
    {
        Password = password;
    }

    public void SetDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}