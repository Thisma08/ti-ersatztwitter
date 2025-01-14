using Application.UseCases.Like;
using Application.UseCases.Like.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MusicParty2.Controllers;

[ApiController]
[Route("api/likes")]
public class LikeController : ControllerBase
{
    private readonly UseCaseFetchByUserAndTweet _useCaseFetchByUserAndTweet;
    private readonly UseCaseCreateLike _useCaseCreateLikee;
    private readonly UseCaseCountLikes _useCaseCountLikes;
    private readonly UseCaseDeleteLike _useCaseDeleteLike;
    
    public LikeController(UseCaseCreateLike useCaseCreateLikee, UseCaseCountLikes useCaseCountLikes,
        UseCaseDeleteLike useCaseDeleteLike, UseCaseFetchByUserAndTweet useCaseFetchByUserAndTweet)
    {
        _useCaseCreateLikee = useCaseCreateLikee;
        _useCaseCountLikes = useCaseCountLikes;
        _useCaseDeleteLike = useCaseDeleteLike;
        _useCaseFetchByUserAndTweet = useCaseFetchByUserAndTweet;
    }
    
    [HttpGet("{userId}/{tweetId}/exists")]
    public async Task<IActionResult> LikeExists(int userId, int tweetId)
    {
        try
        {
            var exists = await _useCaseFetchByUserAndTweet.Execute(userId, tweetId);
            return Ok(new { Exists = exists });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
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
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpDelete("{userId}/{tweetId}")]
    public async Task<IActionResult> Delete(int userId, int tweetId)
    {
        try
        {
            await _useCaseDeleteLike.Execute(userId, tweetId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}
