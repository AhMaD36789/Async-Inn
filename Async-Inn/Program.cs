using Async_Inn.Data;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.EntityFrameworkCore;

namespace Async_Inn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<AsyncInnDBContext>
                (opions => opions.UseSqlServer(connString));
            builder.Services.AddTransient<IHotel, HotelService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IAminity, AminityService>();
            var app = builder.Build();
            app.MapGet("/", () => "Hello World!");
            app.MapControllers();
            app.Run();
        }
    }
}
