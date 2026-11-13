using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Auth;

public class RegisterRequestDto
{
    [Required]
    [MaxLength( 15 )]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength( 30 )]
    public string SecondName { get; set; } = string.Empty;

    [MaxLength( 30 )]
    public string? MiddleName { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength( 70 )]
    public string Email { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength( 15 )]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [MinLength( 8 )]
    [MaxLength( 128 )]
    public string Password { get; set; } = string.Empty;
}
