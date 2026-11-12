namespace Domain.Interfaces.Models;

public class HotelBookingsReport
{
    public Guid HotelId { get; set; }
    public string HotelName { get; set; } = string.Empty;
    public int BookingsCount { get; set; }
}

public class MonthlyRevenueReport
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalRevenue { get; set; }
}

public class ClientAverageCheckReport
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public decimal AverageCheck { get; set; }
}

public class CityGuestsReport
{
    public string City { get; set; } = string.Empty;
    public int TotalGuests { get; set; }
}

public class CategoryOccupancyReport
{
    public string CategoryName { get; set; } = string.Empty;
    public int BookingsCount { get; set; }
}

public class PopularRatePlanReport
{
    public string RatePlanName { get; set; } = string.Empty;
    public int UsagesCount { get; set; }
}