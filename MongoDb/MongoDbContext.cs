using Microsoft.EntityFrameworkCore;
using MongoDb.Entity;
using MongoDb.Settings;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace MongoDb;


public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoSettings> options)
    {
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
}


