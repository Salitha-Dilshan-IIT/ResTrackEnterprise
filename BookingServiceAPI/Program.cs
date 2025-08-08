using BookingServiceAPI;

var builder = WebApplication.CreateBuilder(args);

// move all service registrations to the extension
builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();

// move the middleware pipeline to the extension
app.UseApp(builder.Environment);

app.Run();
