using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class UpdateRoomCategoryDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    public int MinUserCount { get; set; }

    public int MaxUserCount { get; set; }
}