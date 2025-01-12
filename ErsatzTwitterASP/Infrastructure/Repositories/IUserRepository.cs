using Infrastructure.DbEntities;

namespace Infrastructure.Repositories;

public interface IUserRepository
{
    IEnumerable<DbUser> FetchAll();
}