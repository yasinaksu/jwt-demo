using JwtDemo.AuthServer.Core.Dtos;
using JwtDemo.AuthServer.Core.Models;
using JwtDemo.AuthServer.Core.Services;
using JwtDemo.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        public UserService(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAppAsync(CreateUserDto createUserDto)
        {
            var userApp = new UserApp
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName
            };

            var result = await _userManager.CreateAsync(userApp, createUserDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(error => error.Description).ToList();
                return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
            }
            var userAppDto = ObjectMapper.Mapper.Map<UserAppDto>(userApp);
            return Response<UserAppDto>.Success(userAppDto, 200);
        }

        public async Task<Response<UserAppDto>> GetUserByUserNameAsync(string userName)
        {
            var userApp = await _userManager.FindByNameAsync(userName);
            if (userApp==null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 404, true);
            }

            var userAppDto = ObjectMapper.Mapper.Map<UserAppDto>(userApp);
            return Response<UserAppDto>.Success(userAppDto, 200);
        }
    }
}
