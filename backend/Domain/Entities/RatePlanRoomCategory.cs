namespace Domain.Entities;

public class RatePlanRoomCategory
{
    public Guid Id { get; private set; }
    public Guid RatePlanId { get; private set; }
    public Guid RoomCategoryId { get; private set; }
    public Guid RoomId { get; private set; }
    
    private RatePlanRoomCategory()
    {
    }

    public RatePlanRoomCategory( Guid ratePlanId, Guid roomCategoryId, Guid roomId )
    {
        RatePlanId = ratePlanId;
        RoomCategoryId = roomCategoryId;
        RoomId = roomId;
    }
}