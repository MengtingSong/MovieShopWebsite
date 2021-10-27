using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<User> AddUser(User user);
    }
}