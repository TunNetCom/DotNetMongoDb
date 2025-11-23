using Microsoft.EntityFrameworkCore;
using MongoDb.Entity;

namespace MongoDb.Services;

public class UserService
{
    private readonly MongoDbContext _context;

    public UserService(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task CreateUserAsync(User user)
    {
        _ = _context.Users.Add(user);
        _ = await _context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(int userId, User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (existingUser != null)
        {
            existingUser.UpdateUser(
                name: user.Name,
                lastName: user.LastName,
                birthDate: user.Birthdate);

            _ = _context.Users.Update(existingUser);
            _ = await _context.SaveChangesAsync();
        }
    }

    public async Task AddAddressToUserAsync(int userId, Address address)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            user.Addresses.Add(address);
            _ = _context.Users.Update(user);
            _ = await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveAddressFromUserAsync(int userId, int AddressId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            var addressToRemove = user.Addresses.FirstOrDefault(a => a.Id == AddressId);
            if (addressToRemove != null)
            {
                _ = user.Addresses.Remove(addressToRemove);
                _ = _context.Users.Update(user);
                _ = await _context.SaveChangesAsync();
            }
        }
    }

    public async Task DeleteUserAsync(int userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user != null)
        {
            _ = _context.Users.Remove(user);
            _ = await _context.SaveChangesAsync();
        }
    }
}
