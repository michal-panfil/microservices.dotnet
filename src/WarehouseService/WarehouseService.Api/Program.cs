using WarehouseService.Infrastructure;
using WarehouseService.Infrastructure.Hubs;

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
            builder.Services.AddHttpClients();
            builder.Services.AddSignalRServices(builder.Configuration);
            builder.Services.AddDomainServices(builder.Configuration);
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