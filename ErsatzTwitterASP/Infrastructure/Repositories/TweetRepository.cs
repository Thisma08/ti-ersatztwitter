using Infrastructure.DbEntities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TweetRepository : ITweetRepository
{
    private readonly AppDbContext _context;

    public TweetRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DbTweet>> FetchAll()
    {
        return await _context.Tweets.ToListAsync();
    }
    
    public async Task<DbTweet?> FetchById(int id)
    {
        return await _context.Tweets.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<DbTweet> Create(string content, int userId)
    {
        var tweet = new DbTweet
        {
            Content = content,
            UserId = userId
        };

        await _context.Tweets.AddAsync(tweet); 
        await _context.SaveChangesAsync();

        return tweet;
    }

    public async Task<bool> Delete(int id)
    {
        var tweet = await _context.Tweets.FirstOrDefaultAsync(t => t.Id == id);

        if (tweet == null)
        {
            return false;
        }

        _context.Tweets.Remove(tweet);
        await _context.SaveChangesAsync();
        return true;
    }
}