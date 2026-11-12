using Application.Services;
using Domain.Interfaces.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "api/reports" )]
public class ReportsController : ControllerBase
{
    private readonly ReportService _reportService;

    public ReportsController( ReportService reportService )
    {
        _reportService = reportService;
    }

    [HttpGet( "hotels" )]
    public IActionResult HotelsReport()
    {
        IReadOnlyList<HotelBookingsReport> result = _reportService.GetHotelBookings();
        return Ok( result );
    }

    [HttpGet( "revenue" )]
    public IActionResult RevenueReport()
    {
        IReadOnlyList<MonthlyRevenueReport> result = _reportService.GetMonthlyRevenue();
        return Ok( result );
    }

    [HttpGet( "clients" )]
    public IActionResult ClientsReport()
    {
        IReadOnlyList<ClientAverageCheckReport> result = _reportService.GetClientAverageChecks();
        return Ok( result );
    }

    [HttpGet( "cities" )]
    public IActionResult CitiesReport()
    {
        IReadOnlyList<CityGuestsReport> result = _reportService.GetTopCities();
        return Ok( result );
    }

    [HttpGet( "categories" )]
    public IActionResult CategoriesReport()
    {
        IReadOnlyList<CategoryOccupancyReport> result = _reportService.GetCategoryOccupancy();
        return Ok( result );
    }

    [HttpGet( "rateplans" )]
    public IActionResult RatePlansReport()
    {
        IReadOnlyList<PopularRatePlanReport> result = _reportService.GetPopularRatePlans();
        return Ok( result );
    }
}