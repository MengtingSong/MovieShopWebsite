using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {
        public UserRepository(MovieShopDbContext dbContext): base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        public async Task<Favorite> RemoveFavorite(Favorite favorite)
        {
            throw new NotImplementedException();
        }
    }
}