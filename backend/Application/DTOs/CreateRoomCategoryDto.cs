namespace Application.DTOs;

public class CreateRoomCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public int MinUserCount { get; set; }
    public int MaxUserCount { get; set; }
}