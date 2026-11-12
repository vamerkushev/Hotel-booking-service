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
    private readonly IUserRepository _userRepository;

    public BookingService(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IRatePlanRepository ratePlanRepository,
        IRoomInBookingRepository roomInBookingRepository,
        IUserRepository userRepository )
    {
        _bookingRepository = bookingRepository;
        _roomRepository = roomRepository;
        _ratePlanRepository = ratePlanRepository;
        _roomInBookingRepository = roomInBookingRepository;
        _userRepository = userRepository;
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

        bool isAvailable = _roomInBookingRepository.IsRoomAvailable( dto.RoomId, dto.ArrivalDate, dto.DepartureDate );
        if ( !isAvailable )
        {
            throw new BadRequestException( "Этот номер уже занят на выбранные даты!" );
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
        User? user = _userRepository.FindUserById( userId );
        if ( user == null )
        {
            throw new NotFoundException( $"Пользователь с ID {userId} не найден!" );
        }

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
                Status = b.Status.ToString(),
                RoomId = rib != null ? rib.RoomId : Guid.Empty,
                ArrivalDate = rib != null ? rib.ArrivalDate : DateOnly.MinValue,
                DepartureDate = rib != null ? rib.DepartureDate : DateOnly.MinValue
            };

            result.Add( dto );
        }

        return result;
    }

    public void CancelBooking( Guid bookingId )
    {
        Booking? booking = _bookingRepository.GetById( bookingId );

        if ( booking == null )
        {
            throw new NotFoundException( $"Бронирование с ID {bookingId} не найдено!" );
        }

        if ( booking.Status == Domain.Enums.BookingStatus.CANCELLED )
        {
            throw new BadRequestException( "Это бронирование уже отменено!" );
        }

        booking.Cancel();
        _bookingRepository.Update( booking );
    }
}