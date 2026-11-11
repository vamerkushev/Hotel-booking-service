namespace Application.DTOs;

public class CreateRatePlanRoomCategoryDto
{
    public Guid RatePlanId { get; set; }
    public Guid RoomCategoryId { get; set; }
    public Guid RoomId { get; set; }
}