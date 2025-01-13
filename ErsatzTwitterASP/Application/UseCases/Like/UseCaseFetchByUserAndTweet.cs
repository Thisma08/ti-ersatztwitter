using Infrastructure.Repositories;

namespace Application.UseCases.Like;

public class UseCaseFetchByUserAndTweet
{
    private readonly ILikeRepository _likeRepository;

    public UseCaseFetchByUserAndTweet(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public async Task<bool> Execute(int userId, int tweetId)
    {
        return await _likeRepository.LikeExists(userId, tweetId);
    }
}