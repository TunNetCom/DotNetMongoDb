using Microsoft.AspNetCore.Mvc;
using MongoDb.Entity;
using MongoDb.Services;

namespace MongoDb.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUsers), new { id = user.Id }, user);
    }

    [HttpPut("{userId}/addresses")]
    public async Task<IActionResult> AddAddressToUser(int userId, [FromBody] Address address)
    {
        await _userService.AddAddressToUserAsync(userId, address);
        return NoContent();
    }

    [HttpDelete("{userId}/addresses/{AddressId}")]
    public async Task<IActionResult> RemoveAddressFromUser(int userId, int AddressId)
    {
        await _userService.RemoveAddressFromUserAsync(userId, AddressId);
        return NoContent();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        await _userService.DeleteUserAsync(userId);
        return NoContent();
    }
}
