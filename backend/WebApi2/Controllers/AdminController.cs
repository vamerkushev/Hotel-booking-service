using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi2.Controllers;

[ApiController]
[Route( "api/admin" )]
public class AdminController : ControllerBase
{
    private readonly HotelService _hotelService;
    private readonly RoomService _roomService;
    private readonly UserService _userService;
    private readonly IRoomCategoryRepository _categoryRepository;
    private readonly IRatePlanRepository _ratePlanRepository;
    private readonly IRatePlanRoomCategoryRepository _linkRepository;

    public AdminController(
        HotelService hotelService,
        RoomService roomService,
        UserService userService,
        IRoomCategoryRepository categoryRepository,
        IRatePlanRepository ratePlanRepository,
        IRatePlanRoomCategoryRepository linkRepository )
    {
        _hotelService = hotelService;
        _roomService = roomService;
        _userService = userService;
        _categoryRepository = categoryRepository;
        _ratePlanRepository = ratePlanRepository;
        _linkRepository = linkRepository;
    }

    [HttpPost( "hotel" )]
    public IActionResult CreateHotel( [FromBody] CreateHotelDto dto )
    {
        Guid id = _hotelService.CreateHotel( dto );
        return Ok( id );
    }

    [HttpPost( "room" )]
    public IActionResult CreateRoom( [FromBody] CreateRoomDto dto )
    {
        Guid id = _roomService.CreateRoom( dto );
        return Ok( id );
    }

    [HttpPost( "user" )]
    public IActionResult CreateUser( [FromBody] CreateUserDto dto )
    {
        Guid id = _userService.CreateUser( dto );
        return Ok( id );
    }

    [HttpPost( "category" )]
    public IActionResult CreateCategory( [FromBody] CreateRoomCategoryDto dto )
    {
        RoomCategory category = new RoomCategory( dto.Name, dto.MinUserCount, dto.MaxUserCount );
        _categoryRepository.Save( category );
        return Ok( category.Id );
    }

    [HttpPost( "rateplan" )]
    public IActionResult CreateRatePlan( [FromBody] CreateRatePlanDto dto )
    {
        RatePlan ratePlan = new RatePlan( dto.Name, dto.Description, dto.Price );
        _ratePlanRepository.Save( ratePlan );
        return Ok( ratePlan.Id );
    }

    [HttpPost( "link" )]
    public IActionResult LinkRatePlan( [FromBody] CreateRatePlanRoomCategoryDto dto )
    {
        RatePlanRoomCategory link = new RatePlanRoomCategory( dto.RatePlanId, dto.RoomCategoryId, dto.RoomId );
        _linkRepository.Save( link );
        return Ok( link.Id );
    }
}