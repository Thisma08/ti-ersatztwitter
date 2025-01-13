using Application.UseCases.Tweet.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Repositories;

namespace Application.UseCases.Tweet;

public class UseCaseFetchAllTweets : IUseCaseQuery<IEnumerable<DtoOutputTweet>>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchAllTweets(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DtoOutputTweet>> Execute()
    {
        var tweets = await _tweetRepository.FetchAll();
        return _mapper.Map<IEnumerable<DtoOutputTweet>>(tweets);
    }
}