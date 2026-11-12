using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User;

public class UpdateUserDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string SecondName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Phone { get; set; } = string.Empty;
}