using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class HotelService
{
    private readonly IHotelRepository _hotelRepository;

    public HotelService( IHotelRepository hotelRepository )
    {
        _hotelRepository = hotelRepository;
    }

    public Guid CreateHotel( CreateHotelDto dto )
    {
        Hotel hotel = new Hotel( dto.Name, dto.Country, dto.City, dto.Street, dto.Stars );
        _hotelRepository.Save( hotel );
        return hotel.Id;
    }

    public IEnumerable<HotelDto> SearchHotels( string city )
    {
        return _hotelRepository
            .GetByCity( city )
            .Select( h => new HotelDto { Id = h.Id, Name = h.Name, City = h.City } );
    }
}