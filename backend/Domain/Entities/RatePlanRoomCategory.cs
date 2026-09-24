namespace Domain.Entities;

public class RatePlanRoomCategory
{
    public int Id { get; private set; }
    public int RatePlanId { get; private set; }
    public int RoomCategoryId { get; private set; }

    public RatePlan? RatePlan { get; private set; }
    public RoomCategory? RoomCategory { get; private set; }

    private RatePlanRoomCategory()
    {
    }

    public RatePlanRoomCategory( int ratePlanId, int roomCategoryId )
    {
        RatePlanId = ratePlanId;
        RoomCategoryId = roomCategoryId;
    }
}