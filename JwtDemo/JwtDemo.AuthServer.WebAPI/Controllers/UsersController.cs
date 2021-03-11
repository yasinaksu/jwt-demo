using JwtDemo.AuthServer.Core.Dtos;
using JwtDemo.AuthServer.Core.Services;
using JwtDemo.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : CustomBaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var result = await _userService.CreateUserAppAsync(createUserDto);
            return ActionResultInstance(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var result = await _userService.GetUserByUserNameAsync(HttpContext.User.Identity.Name);
            return ActionResultInstance(result);
        }
    }
}
