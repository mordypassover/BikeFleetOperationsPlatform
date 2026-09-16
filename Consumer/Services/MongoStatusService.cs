using Consumer.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Consumer.Data;

namespace Consumer.Services;

public class MongoStatusService
{
    private readonly IMongoCollection<StationStatus> _collection;

    public MongoStatusService(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);

        var database = client.GetDatabase(settings.Value.DatabaseName);

        _collection = database.GetCollection<StationStatus>(
            settings.Value.CollectionName);
    }

    public async Task AddAsync(
        StationStatus status,
        CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(
            status,
            cancellationToken: cancellationToken);
    }
}