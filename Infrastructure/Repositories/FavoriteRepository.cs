using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesForUser(int userId, int pageSize = 30, int pageIndex = 1)
        {
            var favorites = await _dbContext.Favorites.Where(f => f.UserId == userId).ToListAsync();
            return favorites;
        }
    }
}