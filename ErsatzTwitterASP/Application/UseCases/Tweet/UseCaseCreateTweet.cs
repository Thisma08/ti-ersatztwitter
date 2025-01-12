using Application.UseCases.Tweet.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Repositories;

namespace Application.UseCases.Tweet;

public class UseCaseCreateTweet : IUseCaseWriter<DtoOutputTweet, DtoInputTweet>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateTweet(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public DtoOutputTweet Execute(DtoInputTweet input)
    {
        var tweet = _mapper.Map<Domain.Tweet>(input);
        
        try
        {
            var dbTweet = _tweetRepository.Create(tweet.Content, tweet.UserId);
            return _mapper.Map<DtoOutputTweet>(dbTweet);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An error occurred: {ex.Message}");
        }
    }
}