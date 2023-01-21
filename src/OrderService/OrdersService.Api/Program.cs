
using OrdersService.Core.Interfaces;
using OrdersService.Infrastructure.Data;
using OrdersService.Infrastructure.HostedServices;
using Microsoft.EntityFrameworkCore;
using OrdersService.Infrastructure.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrdersService.Infrastructure;

namespace OrdersService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");


        }));
        // Add services to the container.
        // builder.Services.AddHostedService<MessageReceiverService>();
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddMessageBus(builder.Configuration);
        builder.Services.AddSingleton(() => new JsonSerializerOptions(JsonSerializerDefaults.Web)
        {
            Converters = { new JsonStringEnumConverter(), }
        });

        builder.Services.AddCors(o => o.AddPolicy("MyPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");


        }));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());



        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
