using OrdersService.Infrastructure.HostedServices;
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

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddHostedService<MessageReceiverService>();
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddMessageBus(builder.Configuration);
        builder.Services.AddDataServices(builder.Configuration);

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

        app.MigrateDatabase();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
