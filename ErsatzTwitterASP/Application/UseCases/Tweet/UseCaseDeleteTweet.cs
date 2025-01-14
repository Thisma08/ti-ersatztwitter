using Application.UseCases.Utils;
using AutoMapper;
using Domain.Services;
using Infrastructure.Repositories;

namespace Application.UseCases.Tweet;

public class UseCaseDeleteTweet : IUseCaseParametrizedQuery<bool, (int tweetId, int connectedUserId)>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;
    private readonly TweetService _tweetService;

    public UseCaseDeleteTweet(ITweetRepository tweetRepository, IMapper mapper, TweetService tweetService)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
        _tweetService = tweetService;
    }

    public async Task<bool> Execute((int tweetId, int connectedUserId) parameters)
    {
        var (tweetId, connectedUserId) = parameters;

        var tweet = await _tweetRepository.FetchById(tweetId);

        if (tweet == null)
        {
            throw new KeyNotFoundException("Tweet not found");
        }

        _tweetService.CheckIfDeletionIsValid(tweet.UserId, connectedUserId);

        return await _tweetRepository.Delete(tweetId);
    }
}