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
    public ActionResult<IEnumerable<DtoOutputUser>> FetchAll()
    {
        try
        {
            return Ok(_useCaseFetchAllUsers.Execute());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}