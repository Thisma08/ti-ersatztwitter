using Application.UseCases.Like.DTOs;
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

    public async Task<bool> Execute(DtoInputLike input)
    {
        return await _likeRepository.Delete(input.UserId, input.TweetId);
    }
}