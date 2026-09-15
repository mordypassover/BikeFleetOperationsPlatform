using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace producer.Services;

public class TaskManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IConfiguration _configuration;
    private readonly IProducer<Null, string> _producer;

    public TaskManager(
        IServiceProvider serviceProvider,
        IConfiguration configuration,
        IProducer<Null, string> producer)
    {
        _serviceProvider = serviceProvider;
        _configuration = configuration;
        _producer = producer;
    }

    public async Task StartAsync()
    {
        var hourlyTask = RunHourlyAsync();
        var minuteTask = RunEveryMinuteAsync();

        await Task.WhenAll(hourlyTask, minuteTask);
    }

    private async Task RunHourlyAsync()
    {
        while (true)
        {
            await ProduceStationInformationAsync();
            await ProduceVehicleTypesAsync();

            await Task.Delay(TimeSpan.FromHours(1));
        }
    }

    private async Task RunEveryMinuteAsync()
    {
        while (true)
        {
            await ProduceStationStatusAsync();

            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }

    private async Task ProduceStationInformationAsync()
    {
        var topic = _configuration["Topics:StationInformation"];

        using var scope = _serviceProvider.CreateScope();

        var service = scope.ServiceProvider
            .GetRequiredService<StationInformationService>();

        var stations = await service.GetStationInfoAsync();

        foreach (var station in stations)
        {
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(station)
            };

            await _producer.ProduceAsync(topic, message);

            Console.WriteLine($"Produced Station Information: {message.Value}");
        }
    }

    private async Task ProduceVehicleTypesAsync()
    {
        var topic = _configuration["Topics:VehicleTypes"];

        using var scope = _serviceProvider.CreateScope();

        var service = scope.ServiceProvider
            .GetRequiredService<VehicleTypeService>();

        var vehicleTypes = await service.GetVehicleTypesAsync();

        foreach (var vehicleType in vehicleTypes)
        {
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(vehicleType)
            };

            await _producer.ProduceAsync(topic, message);

            Console.WriteLine($"Produced Vehicle Type: {message.Value}");
        }
    }

    private async Task ProduceStationStatusAsync()
    {
        var topic = _configuration["Topics:StationStatus"];

        using var scope = _serviceProvider.CreateScope();

        var service = scope.ServiceProvider
            .GetRequiredService<StationStatusService>();

        var statuses = await service.GetStationStatusAsync();

        foreach (var status in statuses)
        {
            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(status)
            };

            await _producer.ProduceAsync(topic, message);

            Console.WriteLine($"Produced Station Status: {message.Value}");
        }
    }
}