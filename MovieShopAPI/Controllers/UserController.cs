using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MovieShopAPI.Services;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [Authorize]
        [HttpGet("purchases")]
        public async Task<IActionResult> GetUserPurchases()
        {
            var userId = _currentUserService.UserId;
            var purchases = await _userService.GetAllPurchasesForUser(userId);
            if (purchases == null)
            {
                return NotFound("No Purchases Found");
            }
            return Ok(purchases);
        }
    }
}