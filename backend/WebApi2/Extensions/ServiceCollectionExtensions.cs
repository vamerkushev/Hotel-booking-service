using System.Text;
using System.Text.Json.Serialization;
using Application.Services;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infrastructure.Foundation;
using Infrastructure.Foundation.Repositories;
using Infrastructure.Foundation.Security;
using Infrastructure.Foundation.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using WebApi2.ExceptionHandlers;

namespace WebApi2.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence( this IServiceCollection services, IConfiguration configuration )
    {
        services.AddDbContext<HotelManagementDbContext>( options =>
            options.UseSqlServer( configuration.GetConnectionString( "HotelManagement" ) ) );

        services.AddScoped<IUserRepository, EFUserRepository>();
        services.AddScoped<IHotelRepository, EFHotelRepository>();
        services.AddScoped<IRoomRepository, EFRoomRepository>();
        services.AddScoped<IBookingRepository, EFBookingRepository>();
        services.AddScoped<IRatePlanRepository, EFRatePlanRepository>();
        services.AddScoped<IRoomInBookingRepository, EFRoomInBookingRepository>();
        services.AddScoped<IRoomCategoryRepository, EFRoomCategoryRepository>();
        services.AddScoped<IRatePlanRoomCategoryRepository, EFRatePlanRoomCategoryRepository>();
        services.AddScoped<IReportRepository, EFReportRepository>();
        services.AddScoped<IRefreshTokenRepository, EFRefreshTokenRepository>();
        services.AddScoped<AdminSeeder>();

        return services;
    }

    public static IServiceCollection AddApplicationServices( this IServiceCollection services )
    {
        services.AddScoped<HotelService>();
        services.AddScoped<RoomService>();
        services.AddScoped<BookingService>();
        services.AddScoped<UserService>();
        services.AddScoped<ReportService>();
        services.AddScoped<AuthService>();

        return services;
    }

    public static IServiceCollection AddJwtAuthentication( this IServiceCollection services, IConfiguration configuration )
    {
        IConfigurationSection section = configuration.GetSection( JwtOptions.SectionName );
        JwtOptions jwtOptions = section.Get<JwtOptions>()
            ?? throw new InvalidOperationException( $"Configuration section '{JwtOptions.SectionName}' is missing" );

        if ( Encoding.UTF8.GetByteCount( jwtOptions.SigningKey ) < JwtOptions.MinSigningKeyBytes )
        {
            throw new InvalidOperationException(
                $"Jwt:SigningKey must be at least {JwtOptions.MinSigningKeyBytes} bytes long" );
        }

        services.Configure<JwtOptions>( section );
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<ITokenService, JwtTokenService>();

        services
            .AddAuthentication( JwtBearerDefaults.AuthenticationScheme )
            .AddJwtBearer( options =>
            {
                options.MapInboundClaims = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey( Encoding.UTF8.GetBytes( jwtOptions.SigningKey ) ),
                    ValidAlgorithms = [ SecurityAlgorithms.HmacSha256 ],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds( 30 ),
                    NameClaimType = JwtRegisteredClaimNames.Sub,
                    RoleClaimType = JwtTokenService.RoleClaimType
                };
            } );

        services.AddAuthorization();

        return services;
    }

    public static IServiceCollection AddWebApi( this IServiceCollection services )
    {
        services.AddExceptionHandler<GlobalExceptionHandlers>();
        services.AddProblemDetails();

        services.AddControllers()
            .AddJsonOptions( options => options.JsonSerializerOptions.Converters.Add( new JsonStringEnumConverter() ) );

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen( options =>
        {
            options.AddSecurityDefinition( JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Description = "Access token from /api/auth/login"
            } );

            options.AddSecurityRequirement( document => new OpenApiSecurityRequirement
            {
                [ new OpenApiSecuritySchemeReference( JwtBearerDefaults.AuthenticationScheme, document ) ] = []
            } );
        } );

        return services;
    }
}
