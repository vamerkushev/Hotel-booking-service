using Domain.Interfaces.Models;

namespace Domain.Interfaces.Repositories;

public interface IReportRepository
{
    IReadOnlyList<HotelBookingsReport> GetBookingsCountByHotel();
    IReadOnlyList<MonthlyRevenueReport> GetRevenueByMonth();
    IReadOnlyList<ClientAverageCheckReport> GetAverageCheckByClient();
    IReadOnlyList<CityGuestsReport> GetTopCitiesByGuests();
    IReadOnlyList<CategoryOccupancyReport> GetRoomCategoryOccupancy();
    IReadOnlyList<PopularRatePlanReport> GetPopularRatePlans();
}