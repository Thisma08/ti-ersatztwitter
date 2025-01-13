using Application.UseCases.Like;
using Application.UseCases.Like.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MusicParty2.Controllers;

[ApiController]
[Route("api/likes")]
public class LikeController : ControllerBase
{
    private readonly UseCaseCreateLike _useCaseCreateLikee;
    private readonly UseCaseCountLikes _useCaseCountLikes;
    private readonly UseCaseDeleteLike _useCaseDeleteLike;



    public LikeController(UseCaseCreateLike useCaseCreateLikee, UseCaseCountLikes useCaseCountLikes, UseCaseDeleteLike useCaseDeleteLike)
    {
        _useCaseCreateLikee = useCaseCreateLikee;
        _useCaseCountLikes = useCaseCountLikes;
        _useCaseDeleteLike = useCaseDeleteLike;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> CountVotes(int id)
    {
        var output = await _useCaseCountLikes.Execute(id);
        return Ok(output);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLike([FromBody] DtoInputLike input)
    {
        if (input == null)
        {
            return BadRequest("Input cannot be null.");
        }
        try
        {
            var output = await _useCaseCreateLikee.Execute(input);
            return Ok(output);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DtoInputLike input)
    {
        try
        {
            await _useCaseDeleteLike.Execute(input);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}
