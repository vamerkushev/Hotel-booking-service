using Application.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class RoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IHotelRepository _hotelRepository;

    public RoomService( IRoomRepository roomRepository, IHotelRepository hotelRepository )
    {
        _roomRepository = roomRepository;
        _hotelRepository = hotelRepository;
    }

    public Guid CreateRoom( CreateRoomDto dto )
    {
        Room room = new Room( dto.HotelId, dto.RoomCategoryId, dto.Number );
        _roomRepository.Save( room );
        return room.Id;
    }

    public IEnumerable<RoomDto> SearchRooms( Guid hotelId, DateOnly arrival, DateOnly departure )
    {
        if ( arrival >= departure )
        {
            throw new BadRequestException( "Дата выезда должна быть строго позже даты заезда!" );
        }

        if ( arrival < DateOnly.FromDateTime( DateTime.UtcNow ) )
        {
            throw new BadRequestException( "Дата заезда не может быть в прошлом!" );
        }

        Hotel? hotel = _hotelRepository.GetById( hotelId );
        if ( hotel == null )
        {
            throw new NotFoundException( $"Отель с ID {hotelId} не найден!" );
        }

        IReadOnlyList<Room> rooms = _roomRepository.GetAvailableRooms( hotelId, arrival, departure );

        List<RoomDto> result = new List<RoomDto>();
        foreach ( var r in rooms )
        {
            result.Add( new RoomDto { Id = r.Id, Number = r.Number } );
        }

        return result;
    }

    public RoomDto GetById( Guid id )
    {
        Room? room = _roomRepository.GetById( id );

        if ( room == null )
        {
            throw new NotFoundException( $"Комната с ID {id} не найдена!" );
        }

        RoomDto dto = new RoomDto();
        dto.Id = room.Id;
        dto.Number = room.Number;

        return dto;
    }
}