using Infrastructure.DbEntities;

namespace Infrastructure.Repositories;

public interface ITweetRepository
{
    IEnumerable<DbTweet> FetchAll();
    DbTweet Create(string content, int userId);
    bool Delete(int id);
}