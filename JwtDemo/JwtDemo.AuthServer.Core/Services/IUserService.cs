using JwtDemo.AuthServer.Core.Dtos;
using JwtDemo.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAppAsync(CreateUserDto createUserDto);
        Task<Response<UserAppDto>> GetUserByUserNameAsync(string userName);
    }
}
