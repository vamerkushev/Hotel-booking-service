using Domain.Interfaces.Models;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class ReportService
{
    private readonly IReportRepository _reportRepository;

    public ReportService( IReportRepository reportRepository )
    {
        _reportRepository = reportRepository;
    }


    public IReadOnlyList<HotelBookingsReport> GetHotelBookings() => _reportRepository.GetBookingsCountByHotel();

    public IReadOnlyList<MonthlyRevenueReport> GetMonthlyRevenue() => _reportRepository.GetRevenueByMonth();

    public IReadOnlyList<ClientAverageCheckReport> GetClientAverageChecks() => _reportRepository.GetAverageCheckByClient();

    public IReadOnlyList<CityGuestsReport> GetTopCities() => _reportRepository.GetTopCitiesByGuests();

    public IReadOnlyList<CategoryOccupancyReport> GetCategoryOccupancy() => _reportRepository.GetRoomCategoryOccupancy();

    public IReadOnlyList<PopularRatePlanReport> GetPopularRatePlans() => _reportRepository.GetPopularRatePlans();
}