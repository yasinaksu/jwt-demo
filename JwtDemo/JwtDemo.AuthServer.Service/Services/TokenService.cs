using JwtDemo.AuthServer.Core.Configuration;
using JwtDemo.AuthServer.Core.Dtos;
using JwtDemo.AuthServer.Core.Models;
using JwtDemo.AuthServer.Core.Services;
using JwtDemo.Shared.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JwtDemo.AuthServer.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _customTokenOption;
        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _customTokenOption = options.Value;
        }

        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);
            var base64String = Convert.ToBase64String(numberByte);
            return base64String;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            throw new NotImplementedException();
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            throw new NotImplementedException();
        }
    }
}
