using BookingServiceAPI.Data;
using BookingServiceAPI.Repositories;
using BookingServiceAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookingServiceAPI;

public static class StartupExtensions
{
    // Services (what used to be in builder.Services...)
    public static void AddAppServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<BookingDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<BookingService>();
        services.AddScoped<ISpecialRequestRepository, SpecialRequestRepository>();
        services.AddScoped<SpecialRequestService>();

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
