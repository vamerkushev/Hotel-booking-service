using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class RoomService
{
    private readonly IRoomRepository _roomRepository;

    public RoomService( IRoomRepository roomRepository )
    {
        _roomRepository = roomRepository;
    }

    public Guid CreateRoom( CreateRoomDto dto )
    {
        Room room = new Room( dto.HotelId, dto.RoomCategoryId, dto.Number );
        _roomRepository.Save( room );
        return room.Id;
    }

    public IEnumerable<RoomDto> SearchRooms( Guid hotelId, DateOnly arrival, DateOnly departure )
    {
        return _roomRepository
            .GetAvailableRooms( hotelId, arrival, departure )
            .Select( r => new RoomDto { Id = r.Id, Number = r.Number } );
    }
}