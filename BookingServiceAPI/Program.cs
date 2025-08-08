using BookingServiceAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAppServices(builder.Configuration);

var app = builder.Build();


app.UseApp(builder.Environment);

app.Run();
