using Application.DTOs;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;

namespace Application.Services;

public class BookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IRatePlanRepository _ratePlanRepository;
    private readonly IRoomInBookingRepository _roomInBookingRepository;

    public BookingService(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IRatePlanRepository ratePlanRepository,
        IRoomInBookingRepository roomInBookingRepository )
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _ratePlanRepository = ratePlanRepository;
        _roomInBookingRepository = roomInBookingRepository;
    }

    public Guid CreateBooking( CreateBookingDto dto )
    {
        Room? room = _roomRepository.GetById( dto.RoomId );
        if ( room == null )
        {
            throw new NotFoundException( "Комната не найдена!" );
        }

        RatePlan? ratePlan = _ratePlanRepository.GetByRoomCategoryId( room.RoomCategoryId );
        if ( ratePlan == null )
        {
            throw new NotFoundException( "Тариф для данной категории номера не найден!" );
        }

        int nights = dto.DepartureDate.DayNumber - dto.ArrivalDate.DayNumber;
        if ( nights <= 0 )
        {
            throw new BadRequestException( "Дата выезда должна быть позже даты заезда!" );
        }

        decimal totalPrice = ratePlan.Price * nights;

        Booking booking = new Booking(
            dto.UserId,
            dto.UserCount,
            Domain.Enums.CurrencyList.RUB,
            dto.ArrivalDate,
            Domain.Enums.BookingStatus.CREATED
        );

        booking.SetTotal( totalPrice );
        _bookingRepository.Save( booking );

        RoomInBooking roomInBooking = new RoomInBooking(
            booking.Id,
            room.Id,
            dto.ArrivalDate,
            dto.DepartureDate
        );

        _roomInBookingRepository.Save( roomInBooking );

        return booking.Id;
    }

    public IEnumerable<BookingDto> GetUserBookings( Guid userId )
    {
        IReadOnlyList<Booking> bookings = _bookingRepository.GetByUserId( userId );

        List<BookingDto> result = new List<BookingDto>();

        foreach ( Booking b in bookings )
        {
            IReadOnlyList<RoomInBooking> roomsInBooking = _roomInBookingRepository.GetByBookingId( b.Id );

            RoomInBooking? rib = roomsInBooking.FirstOrDefault();

            BookingDto dto = new BookingDto
            {
                Id = b.Id,
                Total = b.Total,
                Currency = b.Currency.ToString(),
                RoomId = rib != null ? rib.RoomId : Guid.Empty,
                ArrivalDate = rib != null ? rib.ArrivalDate : DateOnly.MinValue,
                DepartureDate = rib != null ? rib.DepartureDate : DateOnly.MinValue
            };

            result.Add( dto );
        }

        return result;
    }
}