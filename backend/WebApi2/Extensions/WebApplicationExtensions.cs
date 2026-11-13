using Infrastructure.Foundation;
using Infrastructure.Foundation.Seeding;
using Microsoft.EntityFrameworkCore;

namespace WebApi2.Extensions;

public static class WebApplicationExtensions
{
    public static void InitializeDatabase( this WebApplication app )
    {
        using IServiceScope scope = app.Services.CreateScope();

        scope.ServiceProvider.GetRequiredService<HotelManagementDbContext>().Database.Migrate();

        AdminSeedOptions adminSeed = app.Configuration.GetSection( AdminSeedOptions.SectionName ).Get<AdminSeedOptions>()
            ?? new AdminSeedOptions();
        scope.ServiceProvider.GetRequiredService<AdminSeeder>().Seed( adminSeed );
    }
}
