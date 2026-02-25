using Microsoft.EntityFrameworkCore;
using TaskEngine.Domain.Entities;
using TaskEngine.Domain.Interfaces;
using TaskEngine.Infrastructure.Persistence;

namespace TaskEngine.Infrastructure.Repositories;

public class UserRepository : Repository<User,int>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}
