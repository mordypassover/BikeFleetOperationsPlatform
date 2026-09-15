using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using producer.Services;
using System;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true)
    .AddEnvironmentVariables()
    .Build();

Console.WriteLine($"BootstrapServers = '{configuration["BootstrapServers"]}'");
Console.WriteLine($"ENV BootstrapServers = '{Environment.GetEnvironmentVariable("BootstrapServers")}'");

var services = new ServiceCollection();

services.AddHttpClient();

services.AddScoped<VehicleTypeService>();
services.AddScoped<StationInformationService>();
services.AddScoped<StationStatusService>();
services.AddSingleton<IConfiguration>(configuration);


services.AddSingleton<IProducer<Null, string>>(sp =>
{
    var configService = sp.GetRequiredService<IConfiguration>();
    var bootstrapServers = configService["BootstrapServers"];

    Console.WriteLine($"Kafka BootstrapServers: '{bootstrapServers}'");

    if (string.IsNullOrWhiteSpace(bootstrapServers))
    {
        throw new InvalidOperationException(
            "BootstrapServers configuration setting is missing or empty."
        );
    }

    var config = new ProducerConfig
    {
        BootstrapServers = bootstrapServers
    };

    return new ProducerBuilder<Null, string>(config).Build();
});



services.AddSingleton<TaskManager>();

var serviceProvider = services.BuildServiceProvider();

var taskManager = serviceProvider.GetRequiredService<TaskManager>();

await taskManager.StartAsync();