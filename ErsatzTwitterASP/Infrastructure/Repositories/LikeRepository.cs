using Infrastructure.DbEntities;
 using Microsoft.EntityFrameworkCore;
 
 namespace Infrastructure.Repositories;
 
 public class LikeRepository: ILikeRepository
 {
     private readonly AppDbContext _context;
 
     public LikeRepository(AppDbContext context)
     {
         _context = context;
     }
     
     public async Task<DbLike> Create(int userId, int musicId)
     {
         var like = new DbLike
         {
             UserId = userId,
             TweetId = musicId
         };
         _context.Likes.Add(like);
         await _context.SaveChangesAsync();
         return like;
     }
     
     public async Task<DbUser> FetchUserById(int userId)
     {
         return await _context.Users.FindAsync(userId);
     }

     public async Task<DbTweet> FetchTweetById(int tweetId)
     {
         return await _context.Tweets.FindAsync(tweetId);
     }
     
     public async Task<bool> Delete(int userId, int tweetId)
     {
         var like = await _context.Likes
             .FirstOrDefaultAsync(l => l.UserId == userId && l.TweetId == tweetId);
    
         if (like == null)
         {
             return false;
         }
    
         _context.Likes.Remove(like);
         await _context.SaveChangesAsync();
         return true;
     }
     
     public async Task<int> CountLikes(int musicId)
     {
         return await _context.Likes.CountAsync(l => l.TweetId == musicId);
     }
 }