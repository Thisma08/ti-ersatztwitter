using Application.UseCases.Like.DTOs;
using AutoMapper;
using Infrastructure.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Like;

public class UseCaseCreateLike
{
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public UseCaseCreateLike(ILikeRepository repository, IMapper mapper)
    {
        _likeRepository = repository;
        _mapper = mapper;
    }
    
    public async Task<DtoOutputLike> Execute(DtoInputLike input)
    {
        if (input == null)
            throw new ArgumentNullException(nameof(input));
        
        var likeExists = await _likeRepository.Exists(input.UserId, input.TweetId);
        if (likeExists)
        {
            throw new InvalidOperationException("This like has already been made by this person on this tweet.");
        }
        
        var dbUser = await _likeRepository.FetchUserById(input.UserId);
        var dbTweet = await _likeRepository.FetchTweetById(input.TweetId);
        
        var user = _mapper.Map<Domain.User>(dbUser);
        var tweet = _mapper.Map<Domain.Tweet>(dbTweet);
        
        var like = new Domain.Like
        {
            UserId = input.UserId,
            TweetId = input.TweetId,
            Tweet = tweet,
            User = user
        };
        
        await _likeRepository.Create(input.UserId, input.TweetId);
        
        return new DtoOutputLike
        {
            UserId = input.UserId,
            TweetId = input.TweetId,
            Message = "Like created successfully!"
        };
        
    }
}