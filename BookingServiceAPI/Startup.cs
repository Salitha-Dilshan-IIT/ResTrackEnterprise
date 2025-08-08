using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookingServiceAPI;

public class Startup
{
    private readonly IConfiguration _config;
    public Startup(IConfiguration config) => _config = config;

    // Used by the CLI at build-time
    public void ConfigureServices(IServiceCollection services)
        => services.AddAppServices(_config);

    // Used by the CLI at build-time
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        => app.UseApp(env);
}
