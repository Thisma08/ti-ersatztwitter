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
         var vote = new DbLike
         {
             UserId = userId,
             TweetId = musicId
         };
         _context.Likes.Add(vote);
         await _context.SaveChangesAsync();
         return vote;
     }
     
     public async Task<DbUser> FetchUserById(int userId)
     {
         return await _context.Users.FindAsync(userId);
     }

     public async Task<DbTweet> FetchTweetById(int tweetId)
     {
         return await _context.Tweets.FindAsync(tweetId);
     }
     
     public async Task<int> CountLikes(int musicId)
     {
         return await _context.Likes.CountAsync(l => l.TweetId == musicId);
     }
 }