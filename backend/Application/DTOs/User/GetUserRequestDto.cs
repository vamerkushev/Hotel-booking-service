using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.User;

public class GetUserRequestDto
{
    [Required]
    public Guid UserId { get; set; }
}
