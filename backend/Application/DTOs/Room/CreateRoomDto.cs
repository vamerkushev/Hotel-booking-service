namespace Application.DTOs.Room;

public class CreateRoomDto
{
    public Guid HotelId { get; set; }
    public Guid RoomCategoryId { get; set; }
    public string Number { get; set; } = string.Empty;
}