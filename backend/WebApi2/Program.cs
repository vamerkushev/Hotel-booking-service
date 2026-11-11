using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Foundation.Security;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using WebApi2.ExceptionHandlers;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

builder.Services.AddDbContext<HotelManagementDbContext>( options =>
    options.UseSqlServer( builder.Configuration.GetConnectionString( "HotelManagement" ) ) );

builder.Services.AddExceptionHandler<GlobalExceptionHandlers>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IHotelRepository, EFHotelRepository>();
builder.Services.AddScoped<IRoomRepository, EFRoomRepository>();
builder.Services.AddScoped<IBookingRepository, EFBookingRepository>();
builder.Services.AddScoped<IRatePlanRepository, EFRatePlanRepository>();
builder.Services.AddScoped<IRoomInBookingRepository, EFRoomInBookingRepository>();
builder.Services.AddScoped<IRoomCategoryRepository, EFRoomCategoryRepository>();
builder.Services.AddScoped<IRatePlanRoomCategoryRepository, EFRatePlanRoomCategoryRepository>();

builder.Services.AddScoped<HotelService>();
builder.Services.AddScoped<RoomService>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseExceptionHandler();

if ( app.Environment.IsDevelopment() )
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();