using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Repositories;

namespace Application.UseCases.Tweet;

public class UseCaseDeleteTweet : IUseCaseParametrizedQuery<bool, int>
{
    private readonly ITweetRepository _tweetRepository;
    private readonly IMapper _mapper;

    public UseCaseDeleteTweet(ITweetRepository tweetRepository, IMapper mapper)
    {
        _tweetRepository = tweetRepository;
        _mapper = mapper;
    }

    public async Task<bool> Execute(int id)
    {
        return await _tweetRepository.Delete(id);
    }
}