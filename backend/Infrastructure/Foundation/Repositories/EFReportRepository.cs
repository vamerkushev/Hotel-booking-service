using Domain.Enums;
using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Infrastructure.Foundation.Repositories;

public class EFReportRepository : IReportRepository
{
    private readonly HotelManagementDbContext _dbContext;

    public EFReportRepository( HotelManagementDbContext dbContext )
    {
        _dbContext = dbContext;
    }

    public IReadOnlyList<HotelBookingsReport> GetBookingsCountByHotel()
    {
        var query = from h in _dbContext.Set<Domain.Entities.Hotel>()
                    join r in _dbContext.Set<Domain.Entities.Room>() on h.Id equals r.HotelId into rooms
                    from r in rooms.DefaultIfEmpty()
                    join rib in _dbContext.Set<Domain.Entities.RoomInBooking>() on r.Id equals rib.RoomId into roomBookings
                    from rib in roomBookings.DefaultIfEmpty()
                    join b in _dbContext.Set<Domain.Entities.Booking>() on rib.BookingId equals b.Id into bookings
                    from b in bookings.DefaultIfEmpty()
                    group new { rib, b } by new { h.Id, h.Name } into g
                    select new HotelBookingsReport
                    {
                        HotelId = g.Key.Id,
                        HotelName = g.Key.Name,
                        BookingsCount = g.Where( x => x.rib != null && x.b != null && x.b.Status != Domain.Enums.BookingStatus.CANCELLED )
                            .Select( x => x.rib.BookingId )
                            .Distinct()
                            .Count()
                    };

        return query.ToList();
    }

    public IReadOnlyList<MonthlyRevenueReport> GetRevenueByMonth()
    {
        var query = from b in _dbContext.Set<Domain.Entities.Booking>()
                    where b.Status != BookingStatus.CANCELLED
                    group b by new { b.BookingDate.Year, b.BookingDate.Month } into g
                    orderby g.Key.Year, g.Key.Month
                    select new MonthlyRevenueReport
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        TotalRevenue = g.Sum( x => x.Total )
                    };

        return query.ToList();
    }

    public IReadOnlyList<ClientAverageCheckReport> GetAverageCheckByClient()
    {
        var query = from b in _dbContext.Set<Domain.Entities.Booking>()
                    where b.Status != BookingStatus.CANCELLED
                    join u in _dbContext.Set<Domain.Entities.User>() on b.UserId equals u.Id
                    group b by new { u.Id, u.FirstName, u.SecondName } into g
                    select new ClientAverageCheckReport
                    {
                        UserId = g.Key.Id,
                        UserName = g.Key.FirstName + " " + g.Key.SecondName,
                        AverageCheck = g.Average( x => x.Total )
                    };

        return query.ToList();
    }

    public IReadOnlyList<CityGuestsReport> GetTopCitiesByGuests()
    {
        var query = from rib in _dbContext.Set<Domain.Entities.RoomInBooking>()
                    join b in _dbContext.Set<Domain.Entities.Booking>() on rib.BookingId equals b.Id
                    join r in _dbContext.Set<Domain.Entities.Room>() on rib.RoomId equals r.Id
                    join h in _dbContext.Set<Domain.Entities.Hotel>() on r.HotelId equals h.Id
                    where b.Status != BookingStatus.CANCELLED
                    group b by h.City into g
                    select new CityGuestsReport
                    {
                        City = g.Key,
                        TotalGuests = g.Sum( x => x.UserCount )
                    };

        return query.OrderByDescending( x => x.TotalGuests ).ToList();
    }

    public IReadOnlyList<CategoryOccupancyReport> GetRoomCategoryOccupancy()
    {
        var query = from rib in _dbContext.Set<Domain.Entities.RoomInBooking>()
                    join r in _dbContext.Set<Domain.Entities.Room>() on rib.RoomId equals r.Id
                    join rc in _dbContext.Set<Domain.Entities.RoomCategory>() on r.RoomCategoryId equals rc.Id
                    join b in _dbContext.Set<Domain.Entities.Booking>() on rib.BookingId equals b.Id
                    where b.Status != BookingStatus.CANCELLED
                    group rib by rc.Name into g
                    select new CategoryOccupancyReport
                    {
                        CategoryName = g.Key,
                        BookingsCount = g.Count()
                    };

        return query.ToList();
    }

    public IReadOnlyList<PopularRatePlanReport> GetPopularRatePlans()
    {
        var query = from rib in _dbContext.Set<Domain.Entities.RoomInBooking>()
                    join b in _dbContext.Set<Domain.Entities.Booking>() on rib.BookingId equals b.Id
                    join r in _dbContext.Set<Domain.Entities.Room>() on rib.RoomId equals r.Id
                    join rprc in _dbContext.Set<Domain.Entities.RatePlanRoomCategory>() on r.RoomCategoryId equals rprc.RoomCategoryId
                    join rp in _dbContext.Set<Domain.Entities.RatePlan>() on rprc.RatePlanId equals rp.Id
                    where b.Status != BookingStatus.CANCELLED
                    group rp by rp.Name into g
                    select new PopularRatePlanReport
                    {
                        RatePlanName = g.Key,
                        UsagesCount = g.Count()
                    };

        return query.OrderByDescending( x => x.UsagesCount ).ToList();
    }
}