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
    public ActionResult<IEnumerable<DtoOutputTweet>> FetchAll()
    {
        return Ok(_useCaseFetchAllTweets.Execute());
    }

    [HttpPost]
    public ActionResult<DtoOutputTweet> Create(DtoInputTweet input)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            return Ok(_useCaseCreateTweet.Execute(input));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
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

        var tweet = _useCaseFetchAllTweets.Execute().FirstOrDefault(t => t.Id == id);
        if (tweet == null)
        {
            return NotFound(new { message = "Tweet not found" });
        }

        if (tweet.UserId != connectedUserId)
        {
            return Unauthorized(new { message = "You can only delete your own tweets" });
        }

        _useCaseDeleteTweet.Execute(id);
        return NoContent();
    }
}