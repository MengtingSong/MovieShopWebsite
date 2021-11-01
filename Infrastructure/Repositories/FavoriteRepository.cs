using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Favorite>> GetAllFavoritesForUser(int userId, int pageSize = 30, int pageIndex = 1)
        {
            throw new System.NotImplementedException();
        }
    }
}