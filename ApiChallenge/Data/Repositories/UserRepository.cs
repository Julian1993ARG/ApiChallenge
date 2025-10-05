using Microsoft.EntityFrameworkCore;
using ApiChallenge.Data.Entities;

namespace ApiChallenge.Data.Repositories;

public class UserRepository : GenericRepository<User, int>, IUserRepository
{
    public UserRepository(ChallengeDbContext context) : base(context)
    {
    }
}