using Application.UseCases.Like.DTOs;
using AutoMapper;
using Infrastructure.Repositories;

namespace Application.UseCases.Like;

public class UseCaseCountLikes
{
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public UseCaseCountLikes(ILikeRepository repository, IMapper mapper)
    {
        _likeRepository = repository;
        _mapper = mapper;
    }

    public async Task<DtoOutputCountLikes> Execute(int tweetId)
    {
        var likeCount = await _likeRepository.CountLikes(tweetId);
        return new DtoOutputCountLikes
        {
            LikeCount = likeCount
        };
    }
}