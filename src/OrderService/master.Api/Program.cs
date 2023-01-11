
using master.Core.Interfaces;
using master.Infrastructure.Data;
using master.Infrastructure.HostedServices;
using Microsoft.EntityFrameworkCore;
using master.Infrastructure.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using master.Infrastructure;

namespace master.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
       // builder.Services.AddHostedService<MessageReceiverService>();
        builder.Services.AddTransient<IMessageBusReceiver, MessageBusReceiver>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDatabase(builder.Configuration);
        builder.Services.AddSingleton(() => new JsonSerializerOptions(JsonSerializerDefaults.Web)

        {
            Converters = { new JsonStringEnumConverter(), }
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
