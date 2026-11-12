using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UpdateRoomDto
{
    [Required]
    public Guid RoomCategoryId { get; set; }

    [Required]
    public string Number { get; set; } = string.Empty;
}