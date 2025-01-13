using Application.UseCases.User.Dtos;
using Application.UseCases.Utils;
using AutoMapper;
using Infrastructure.Repositories;

namespace Application.UseCases.User;

public class UseCaseFetchAllUsers : IUseCaseQuery<IEnumerable<DtoOutputUser>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UseCaseFetchAllUsers(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<DtoOutputUser>> Execute()
    {
        var users = await _userRepository.FetchAll();
        return _mapper.Map<IEnumerable<DtoOutputUser>>(users);
    }
}