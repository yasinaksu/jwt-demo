using JwtDemo.AuthServer.Core.Configuration;
using JwtDemo.AuthServer.Core.Dtos;
using JwtDemo.AuthServer.Core.Models;
using JwtDemo.AuthServer.Core.Repositories;
using JwtDemo.AuthServer.Core.Services;
using JwtDemo.AuthServer.Core.UnitOfWork;
using JwtDemo.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JwtDemo.AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenRepository;

        public AuthenticationService(IOptions<List<Client>> clientOptions, ITokenService tokenService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> userRefreshTokenRepository)
        {
            _clients = clientOptions.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenRepository = userRefreshTokenRepository;
        }
        public Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
