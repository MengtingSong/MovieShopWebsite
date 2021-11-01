using System.Collections.Generic;

namespace MovieShopMVC.Services
{
    public interface ICurrentUserService
    {
        // expose some properties and methods that can be implemented by CurrentUserService class
        // that will read user info from HttpContext 
        public int UserId { get; }
        public bool IsAuthenticated { get; }
        public string FullName { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }
        public bool IsAdmin { get; }
    }
}