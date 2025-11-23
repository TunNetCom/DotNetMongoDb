using MongoDb.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDb.Services;

public class UserService
{
    private readonly IMongoCollection<User> _userCollection;

    public UserService(MongoDbContext context)
    {
        _userCollection = context.Users;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _userCollection.Find(_ => true).ToListAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        await _userCollection.InsertOneAsync(user);
    }

    public async Task UpdateUserAsync(int userId, User user)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var update = Builders<User>.Update
            .Set(u => u.Name, user.Name)
            .Set(u => u.Birthdate, user.Birthdate)
            .Set(u => u.Addresses, user.Addresses);

        _=await _userCollection.UpdateOneAsync(filter, update);
    }

    public async Task AddAddressToUserAsync(int userId, Address address)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var update = Builders<User>.Update.AddToSet(u => u.Addresses, address);

        _ = await _userCollection.UpdateOneAsync(filter, update);
    }

    public async Task RemoveAddressFromUserAsync(int userId, int AddressId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        var addressFilter = Builders<User>.Filter.Eq("Addresses.Id", AddressId);
        var combinedFilter = Builders<User>.Filter.And(filter, addressFilter);

        var update = Builders<User>.Update.PullFilter(u => u.Addresses, Builders<Address>.Filter.Eq(a => a.Id, AddressId));

        _ = await _userCollection.UpdateOneAsync(combinedFilter, update);
    }

    public async Task DeleteUserAsync(int userId)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Id, userId);
        _ = await _userCollection.DeleteOneAsync(filter);
    }
}

