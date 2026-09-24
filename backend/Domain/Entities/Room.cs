namespace Domain.Entities;

public class Room
{
    public int Id { get; private set; }
    public int HotelId { get; private set; }
    public int RoomCategoryId { get; private set; }
    public string Number { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    public Hotel? Hotel { get; private set; }
    public RoomCategory? RoomCategory { get; private set; }
    public ICollection<RoomInBooking> RoomInBookings { get; private set; } = new List<RoomInBooking>();

    private Room()
    {
    }

    public Room( int hotelId, int roomCategoryId, string number )
    {
        HotelId = hotelId;
        RoomCategoryId = roomCategoryId;
        Number = number;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update( int roomCategoryId, string number )
    {
        RoomCategoryId = roomCategoryId;
        Number = number;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}