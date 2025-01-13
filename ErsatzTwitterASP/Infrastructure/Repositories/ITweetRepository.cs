using Infrastructure.DbEntities;

namespace Infrastructure.Repositories;

public interface ITweetRepository
{
    Task<IEnumerable<DbTweet>> FetchAll();
    Task<DbTweet?> FetchById(int id);
    Task<DbTweet> Create(string content, int userId);
    Task<bool> Delete(int id);
}