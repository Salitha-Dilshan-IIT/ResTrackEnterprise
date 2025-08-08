using HotelServiceAPI;

var builder = WebApplication.CreateBuilder(args);

// move all service registrations to the extension
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

app.UseApp(builder.Environment);

app.Run();
