using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IReviewRepository : IAsyncRepository<Review>
    {
        Task<IEnumerable<Review>> GetAllReviewsForUser(int userId, int pageSize = 30, int pageIndex = 1);
        Task<IEnumerable<Review>> GetAllReviewsForMovie(int movieId, int pageSize = 30, int page = 1);
    }
}