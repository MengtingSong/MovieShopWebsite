using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IFavoriteRepository : IAsyncRepository<Favorite>
    {
        Task<IEnumerable<Favorite>> GetAllFavoritesForUser(int userId, int pageSize = 30, int pageIndex = 1);
    }
}