using System;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieShopDbContext _dbContext;

        public UserRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public Task<User> GetUserByEmail(string email)
        {
            var user = _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
    }
}