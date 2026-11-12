using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.RoomCategory;

public class UpdateRoomCategoryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public int MinUserCount { get; set; }

    public int MaxUserCount { get; set; }
}