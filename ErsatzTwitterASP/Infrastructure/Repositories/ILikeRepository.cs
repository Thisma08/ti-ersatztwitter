using Infrastructure.DbEntities;

namespace Infrastructure.Repositories;

public interface ILikeRepository
{
    Task<DbLike> Create(int userId, int tweetId);
    Task<DbUser> FetchUserById(int userId);
    Task<DbTweet> FetchTweetById(int tweetId);
    Task<bool> Delete(int userId, int tweetId);
    Task<int> CountLikes(int tweetId);
}