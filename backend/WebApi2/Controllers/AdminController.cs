using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Domain.Exceptions;
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

    //hotel
    [HttpPost( "hotel" )]
    public IActionResult CreateHotel( [FromBody] CreateHotelDto dto )
    {
        Guid id = _hotelService.CreateHotel( dto );
        return Ok( id );
    }

    [HttpPut( "hotel/{id:guid}" )]
    public IActionResult UpdateHotel( [FromRoute] Guid id, [FromBody] UpdateHotelDto dto )
    {
        _hotelService.UpdateHotel( id, dto );
        return Ok();
    }

    [HttpDelete( "hotel/{id:guid}" )]
    public IActionResult DeleteHotel( [FromRoute] Guid id )
    {
        _hotelService.DeleteHotel( id );
        return Ok();
    }

    //room
    [HttpPost( "room" )]
    public IActionResult CreateRoom( [FromBody] CreateRoomDto dto )
    {
        Guid id = _roomService.CreateRoom( dto );
        return Ok( id );
    }

    [HttpPut( "room/{id:guid}" )]
    public IActionResult UpdateRoom( [FromRoute] Guid id, [FromBody] UpdateRoomDto dto )
    {
        _roomService.UpdateRoom( id, dto );
        return Ok();
    }

    [HttpDelete( "room/{id:guid}" )]
    public IActionResult DeleteRoom( [FromRoute] Guid id )
    {
        _roomService.DeleteRoom( id );
        return Ok();
    }

    //user
    [HttpPost( "user" )]
    public IActionResult CreateUser( [FromBody] CreateUserDto dto )
    {
        Guid id = _userService.CreateUser( dto );
        return Ok( id );
    }

    [HttpPut( "user/{id:guid}" )]
    public IActionResult UpdateUser( [FromRoute] Guid id, [FromBody] UpdateUserDto dto )
    {
        _userService.UpdateUser( id, dto );
        return Ok();
    }

    [HttpDelete( "user/{id:guid}" )]
    public IActionResult DeleteUser( [FromRoute] Guid id )
    {
        _userService.DeleteUser( id );
        return Ok();
    }

    //category
    [HttpPost( "category" )]
    public IActionResult CreateCategory( [FromBody] CreateRoomCategoryDto dto )
    {
        RoomCategory category = new RoomCategory( dto.Name, dto.MinUserCount, dto.MaxUserCount );
        _categoryRepository.Save( category );
        return Ok( category.Id );
    }

    [HttpPut( "category/{id:guid}" )]
    public IActionResult UpdateCategory( [FromRoute] Guid id, [FromBody] UpdateRoomCategoryDto dto )
    {
        RoomCategory? category = _categoryRepository.GetById( id );
        if ( category == null )
        {
            throw new NotFoundException( "Категория не найдена!" );
        }

        category.Update( dto.Name, dto.MinUserCount, dto.MaxUserCount );
        _categoryRepository.Update( category );

        return Ok();
    }

    [HttpDelete( "category/{id:guid}" )]
    public IActionResult DeleteCategory( [FromRoute] Guid id )
    {
        RoomCategory? category = _categoryRepository.GetById( id );
        if ( category == null )
        {
            throw new NotFoundException( "Категория не найдена!" );
        }

        category.SetDeleted();
        _categoryRepository.Update( category );

        return Ok();
    }

    //rateplan
    [HttpPost( "rateplan" )]
    public IActionResult CreateRatePlan( [FromBody] CreateRatePlanDto dto )
    {
        RatePlan ratePlan = new RatePlan( dto.Name, dto.Description, dto.Price );
        _ratePlanRepository.Save( ratePlan );
        return Ok( ratePlan.Id );
    }

    [HttpPut( "rateplan/{id:guid}" )]
    public IActionResult UpdateRatePlan( [FromRoute] Guid id, [FromBody] UpdateRatePlanDto dto )
    {
        RatePlan? ratePlan = _ratePlanRepository.GetById( id );
        if ( ratePlan == null )
        {
            throw new NotFoundException( "Тариф не найден!" );
        }

        ratePlan.Update( dto.Name, dto.Description, dto.Price );
        _ratePlanRepository.Update( ratePlan );

        return Ok();
    }

    [HttpDelete( "rateplan/{id:guid}" )]
    public IActionResult DeleteRatePlan( [FromRoute] Guid id )
    {
        RatePlan? ratePlan = _ratePlanRepository.GetById( id );
        if ( ratePlan == null )
        {
            throw new NotFoundException( "Тариф не найден!" );
        }

        ratePlan.SetDeleted();
        _ratePlanRepository.Update( ratePlan );

        return Ok();
    }

    //link
    [HttpPost( "link" )]
    public IActionResult LinkRatePlan( [FromBody] CreateRatePlanRoomCategoryDto dto )
    {
        RatePlanRoomCategory link = new RatePlanRoomCategory( dto.RatePlanId, dto.RoomCategoryId, dto.RoomId );
        _linkRepository.Save( link );
        return Ok( link.Id );
    }

    [HttpDelete( "link/{id:guid}" )]
    public IActionResult DeleteLink( [FromRoute] Guid id )
    {
        RatePlanRoomCategory? link = _linkRepository.GetById( id );
        if ( link == null )
        {
            throw new NotFoundException( "Связка не найдена!" );
        }

        _linkRepository.Delete( link );

        return Ok();
    }
}