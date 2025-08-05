using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Define CORS policy name
const string CorsPolicyName = "AllowFrontend";

// ✅ Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, builder =>
    {
        builder
            .WithOrigins("http://localhost:5173") // your React dev server
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Load ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot services
builder.Services.AddOcelot();

var app = builder.Build();


app.UseCors(CorsPolicyName);


// Use Ocelot middleware
await app.UseOcelot();

app.Run();
