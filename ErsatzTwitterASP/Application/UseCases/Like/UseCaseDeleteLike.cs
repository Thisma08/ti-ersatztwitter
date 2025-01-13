using Application.UseCases.Like.DTOs;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Repositories;

namespace Application.UseCases.Like;

public class UseCaseDeleteLike
{
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public UseCaseDeleteLike(ILikeRepository likeRepository, IMapper mapper)
    {
        _likeRepository = likeRepository;
        _mapper = mapper;
    }

    public async Task<bool> Execute(int userId, int tweetId)
    {
        return await _likeRepository.Delete(userId, tweetId);
    }
}