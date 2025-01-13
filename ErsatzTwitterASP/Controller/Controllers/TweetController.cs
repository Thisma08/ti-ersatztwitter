using Application.UseCases.Tweet;
using Application.UseCases.Tweet.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MusicParty2.Controllers;

[ApiController]
[Route("api/tweets")]
public class TweetController : ControllerBase
{
    private readonly UseCaseFetchAllTweets _useCaseFetchAllTweets;
    private readonly UseCaseCreateTweet _useCaseCreateTweet;
    private readonly UseCaseDeleteTweet _useCaseDeleteTweet;

    public TweetController(UseCaseDeleteTweet useCaseDeleteTweet, UseCaseCreateTweet useCaseCreateTweet, UseCaseFetchAllTweets useCaseFetchAllTweets)
    {
        _useCaseDeleteTweet = useCaseDeleteTweet;
        _useCaseCreateTweet = useCaseCreateTweet;
        _useCaseFetchAllTweets = useCaseFetchAllTweets;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DtoOutputTweet>>> FetchAll()
    {
        try
        {
            var tweets = await _useCaseFetchAllTweets.Execute();
            return Ok(tweets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpPost]
    public async Task<ActionResult<DtoOutputTweet>> Create([FromBody] DtoInputTweet input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdTweet = await _useCaseCreateTweet.Execute(input);
            return Ok(createdTweet);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var connectedUserIdCookie = HttpContext.Request.Cookies["ConnectedUserId"];
        if (string.IsNullOrEmpty(connectedUserIdCookie))
        {
            return Unauthorized(new { message = "No user connected" });
        }

        if (!int.TryParse(connectedUserIdCookie, out var connectedUserId))
        {
            return BadRequest(new { message = "Invalid user ID in cookie" });
        }

        try
        {
            var tweets = await _useCaseFetchAllTweets.Execute();
            var tweet = tweets.FirstOrDefault(t => t.Id == id);

            if (tweet == null)
            {
                return NotFound(new { message = "Tweet not found" });
            }

            if (tweet.UserId != connectedUserId)
            {
                return Unauthorized(new { message = "You can only delete your own tweets" });
            }

            await _useCaseDeleteTweet.Execute(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Error = "An unexpected error occurred.", Details = ex.Message });
        }
    }
}