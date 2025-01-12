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

    public IEnumerable<DbTweet> FetchAll()
    {
        return _context.Tweets.ToList();
    }
    
    public DbTweet? FetchById(int id)
    {
        return _context.Tweets.FirstOrDefault(t => t.Id == id);
    }

    public DbTweet Create(string content, int userId)
    {
        var music = new DbTweet
        {
            Content = content,
            UserId = userId
        };
        
        _context.Tweets.Add(music); 
        _context.SaveChanges();

        return music;
    }

    public bool Delete(int id)
    {
        var music = _context.Tweets.FirstOrDefault(m => m.Id == id);

        if (music == null)
        {
            return false;
        }
        
        _context.Tweets.Remove(music);
        _context.SaveChanges();
        return true;
    }
}