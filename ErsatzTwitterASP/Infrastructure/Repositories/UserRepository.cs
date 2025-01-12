using Infrastructure.DbEntities;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<DbUser> FetchAll()
    {
        return _context.Users.ToList();
    }
}