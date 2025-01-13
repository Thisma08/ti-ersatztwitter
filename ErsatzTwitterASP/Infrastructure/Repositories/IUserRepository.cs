using Infrastructure.DbEntities;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<DbUser>> FetchAll();
}