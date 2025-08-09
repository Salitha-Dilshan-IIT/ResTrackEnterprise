using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Define CORS policy name
const string CorsPolicyName = "AllowFrontend";

// Load ocelot.json
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Register CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicyName, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://resttrackweb-bqe3h2bta2erguet.canadacentral-01.azurewebsites.net",
                "https://restrack.online/"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Add Ocelot services
builder.Services.AddOcelot();

var app = builder.Build();

//Apply CORS before Ocelot middleware
app.UseCors(CorsPolicyName);

//Use Ocelot routing
await app.UseOcelot();

app.Run();
