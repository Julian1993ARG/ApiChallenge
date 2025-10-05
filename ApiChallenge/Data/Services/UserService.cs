using ApiChallenge.Data;
using ApiChallenge.Data.Entities;
using ApiChallenge.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Services;

public class UserService : GenericService<User, int>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ChallengeDbContext context) 
        : base(userRepository, context)
    {
        _userRepository = userRepository;
    }
}