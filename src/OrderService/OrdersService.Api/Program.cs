
using OrdersService.Core.Interfaces;
using OrdersService.Infrastructure.Data;
using OrdersService.Infrastructure.HostedServices;
using Microsoft.EntityFrameworkCore;
using OrdersService.Infrastructure.Services;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrdersService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Logging;

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
        builder.Services.AddHostedService<MessageReceiverService>();
        builder.Services.AddControllers();
        

        IdentityModelEventSource.ShowPII = true;
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", "OrderApi");
            });
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "My API",
                Version = "v1"
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
   {
     new OpenApiSecurityScheme
     {
       Reference = new OpenApiReference
       {
         Type = ReferenceType.SecurityScheme,
         Id = "Bearer"
       }
      },
      new string[] { }
    }
  });
        });
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

       // app.MigrateDatabase();
        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseRouting();

        app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());


        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers().RequireAuthorization("ApiScope");

        app.Run();
    }
}
