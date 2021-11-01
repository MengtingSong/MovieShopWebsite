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

        public async Task<IEnumerable<Purchase>> GetPurchases(int id)
        {
           var movies = await _dbContext.Purchases.Where(p => p.UserId == id)
                .Include(p => p.Movie).ToListAsync();
            return movies;
        }
    }
}