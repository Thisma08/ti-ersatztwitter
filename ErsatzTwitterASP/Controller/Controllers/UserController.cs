using Application.UseCases.User;
using Application.UseCases.User.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MusicParty2.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly UseCaseFetchAllUsers _useCaseFetchAllUsers;

    public UserController(UseCaseFetchAllUsers useCaseFetchAllUsers)
    {
        _useCaseFetchAllUsers = useCaseFetchAllUsers;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoOutputUser>>> FetchAll()
    {
        try
        {
            var users = await _useCaseFetchAllUsers.Execute();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
    
    [HttpPost("connect/{userId}")]
    public async Task<IActionResult> Connect(int userId)
    {
        var users = await _useCaseFetchAllUsers.Execute();

        if (!users.Any(u => u.Id == userId))
        {
            return NotFound(new { message = "User not found" });
        }

        Response.Cookies.Append("ConnectedUserId", userId.ToString());
        return Ok(new { message = "User connected", userId });
    }
}