using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReviewRepository : EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Review>> GetAllReviewsForUser(int userId, int pageSize = 30, int pageIndex = 1)
        {
            var reviews = await _dbContext.Reviews
                .Where(r => r.UserId == userId).Include(r => r.Movie).ToListAsync();
            return reviews;
        }
        
        public async Task<IEnumerable<Review>> GetAllReviewsForMovie(int movieId, int pageSize = 30, int page = 1)
        {
            var reviews = await _dbContext.Reviews
                .Where(r => r.MovieId == movieId).Include(r => r.User).ToListAsync();
            return reviews;
        }
    }
}