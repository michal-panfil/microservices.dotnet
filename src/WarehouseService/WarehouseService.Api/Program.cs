using WarehouseService.Core.Interfaces;
using WarehouseService.Infrastructure;
using WarehouseService.Infrastructure.Hubs;
using WarehouseService.Infrastructure.Serices;

namespace WarehouseService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDatabase(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddHttpClient();
            builder.Services.AddScoped<IShipmentClient, ShipmentClient>();
            builder.Services.AddSignalRServices(builder.Configuration);
            builder.Services.AddDomainServices(builder.Configuration);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
                app.UseSwagger();
                app.UseSwaggerUI();

            app.UseCors(builder => builder
                .WithOrigins("http://localhost:5002")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());


            app.MapControllers();
            app.MapHub<ShipmentHub>("/shipmentHub");

            app.Run();
        }
    }
}