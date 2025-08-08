using HotelServiceAPI.Data;
using HotelServiceAPI.Repositories;
using HotelServiceAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HotelServiceAPI;

public static class StartupExtensions
{
    // Services (what used to be in builder.Services...)
    public static void AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<HotelDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IHotelRepository, HotelRepository>();
        services.AddScoped<HotelService>();
        services.AddScoped<IRoomRepository, RoomRepository>();
        services.AddScoped<RoomService>();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    // Middleware (what used to be on the 'app' pipeline)
    public static void UseApp(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Enable Swagger for all envs so the CLI/pipeline can read it reliably
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
