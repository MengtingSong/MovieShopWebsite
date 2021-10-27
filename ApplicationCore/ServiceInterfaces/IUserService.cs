using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);
    }
}