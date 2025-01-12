using Application.UseCases.Like.DTOs;
using Application.UseCases.Tweet.Dtos;
using Application.UseCases.User.Dtos;
using AutoMapper;
using Domain;
using Infrastructure.DbEntities;

namespace Application;

public class ProfileMapper: Profile
{
    public ProfileMapper()
    {
        CreateMap<User, DtoOutputUser>();
        CreateMap<DbUser, DtoOutputUser>();
        CreateMap<DbUser, User>();
        
        CreateMap<Tweet, DtoOutputTweet>();
        CreateMap<DbTweet, DtoOutputTweet>();
        CreateMap<DbTweet, Tweet>();
        CreateMap<DtoInputTweet, Tweet>();
        
        CreateMap<Like, DtoOutputLike>();
        CreateMap<DbLike, DtoOutputLike>();
        CreateMap<DbLike, Like>();
        CreateMap<DtoInputLike, Like>();
    }
    
}