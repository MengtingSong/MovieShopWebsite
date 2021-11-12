using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;

namespace MovieShopAPI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        // we need to use HttpContext class to get all this information from HttpContext User Object

        public int UserId => Convert.ToInt32((_httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.NameId)?.Value));

        // TODO: is this still needed for Jwt?
        public bool IsAuthenticated => _httpContextAccessor.HttpContext != null &&
                                       _httpContextAccessor.HttpContext?.User.Identity != null &&
                                       _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string FullName => _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.GivenName)?.Value
                                  + " " + _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.FamilyName)?.Value;


        public string Email => _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        
        public IEnumerable<string> Roles { get; set; }
        public bool IsAdmin { get; set; }
    }
}