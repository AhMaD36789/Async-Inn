using Async_Inn.Data;
using Async_Inn.Models;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
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

            var connString = builder.Configuration
                .GetConnectionString("DefaultConnection");

            builder.Services.AddIdentity<User, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
            }

            ).AddEntityFrameworkStores<AsyncInnDBContext>();
            builder.Services
                .AddDbContext<AsyncInnDBContext>
            (opions => opions.UseSqlServer(connString));

            builder.Services.AddTransient<IHotel, HotelService>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomService>();
            builder.Services.AddTransient<IUser, IdentityUserService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IAminity, AminityService>();

            builder.Services.AddScoped<JwtTokenService>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = JwtTokenService.GetValidationParameter(builder.Configuration);
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("create", policy => policy.RequireClaim("permissions", "create"));
                options.AddPolicy("read", policy => policy.RequireClaim("permissions", "read"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permissions", "delete"));
                options.AddPolicy("update", policy => policy.RequireClaim("permissions", "update"));
            }
            );

            builder.Services.AddAuthorization();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "Async-Inn",
                    Version = "v1",
                });
            });
            var app = builder.Build();
            app.UseSwagger(options =>
            { options.RouteTemplate = "/api/{documentName}/swagger.json"; });
            app.UseSwaggerUI(options =>
                        {
                            options.SwaggerEndpoint("/api/v1/swagger.json", "Async-Inn");
                            options.RoutePrefix = "docs";
                        });
            app.MapGet("/", () => "Hello World!");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();

        }
    }
}
