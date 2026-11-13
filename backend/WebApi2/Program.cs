using WebApi2.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

builder.Services
    .AddPersistence( builder.Configuration )
    .AddJwtAuthentication( builder.Configuration )
    .AddApplicationServices()
    .AddWebApi();

WebApplication app = builder.Build();

app.UseExceptionHandler();

if ( app.Environment.IsDevelopment() )
{
    app.InitializeDatabase();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
