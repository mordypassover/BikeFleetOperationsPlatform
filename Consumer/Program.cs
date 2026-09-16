using Confluent.Kafka;
using Consumer.Data;
using Consumer.Handlers;
using Consumer.Models;
using Consumer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Text.Json;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

var services = new ServiceCollection();

var connectionString =
    configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");
//dbs
services.AddDbContext<MysqlDbContext>(options =>
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString)));

services.Configure<MongoDbSettings>(
    configuration.GetSection("MongoDb"));

services.AddSingleton<MongoStatusService>();
services.AddScoped<StationInformationService>();
services.AddScoped<VehicleTypesService>();

//handelers
services.AddScoped<StationInformationHandler>();
services.AddScoped<VehicleTypesHandler>();
services.AddScoped<StationStatusHandler>();

//redis
var redisConnection =
    configuration.GetConnectionString("Redis");

services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(redisConnection));

services.AddSingleton<RedisStatusService>();



var serviceProvider = services.BuildServiceProvider();


using (var scope = serviceProvider.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MysqlDbContext>();
    db.Database.EnsureCreated();
}

var config = new ConsumerConfig
{
    BootstrapServers = configuration["Kafka:BootstrapServers"],
    GroupId = configuration["Kafka:GroupId"],
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{

    consumer.Subscribe(new[]
    {
        configuration["Kafka:Topics:StationInformation"],
        configuration["Kafka:Topics:StationStatus"],
        configuration["Kafka:Topics:VehicleTypes"]
    });

    while (true)
    {
        var mesege = consumer.Consume();
        if (mesege == null || mesege.Message == null)
        {
            continue;
        }
        using (var scope = serviceProvider.CreateScope())
        {
            try
            {
                switch (mesege.Topic)
                {
                    case "station-information":
                        var StationInformationHandler = scope.ServiceProvider.GetRequiredService<StationInformationHandler>();
                        var stationinformation = JsonSerializer.Deserialize<StationInformation>(mesege.Message.Value);
                        if (stationinformation != null)
                        {
                            await StationInformationHandler.HandleAsync(stationinformation);
                        }
                        break;

                    case "station-status":
                        var StationStatusHandler = scope.ServiceProvider.GetRequiredService<StationStatusHandler>();
                        var stationStatus = JsonSerializer.Deserialize<StationStatus>(mesege.Message.Value);
                        if (stationStatus != null)
                        {
                            await StationStatusHandler.HandleAsync(stationStatus);
                        }
                        break;

                    case "vehicle-types":
                        var VehicleTypesHandler = scope.ServiceProvider.GetRequiredService<VehicleTypesHandler>();
                        var vehicle = JsonSerializer.Deserialize<VehicleType>(mesege.Message.Value);
                        if (vehicle != null)
                        {
                            await VehicleTypesHandler.HandleAsync(vehicle);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}