using JwtDemo.AuthServer.Core.Configuration;
using JwtDemo.AuthServer.Core.Dtos;
using JwtDemo.AuthServer.Core.Models;
using JwtDemo.AuthServer.Core.Services;
using JwtDemo.Shared.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        private IEnumerable<Claim> GetClaims(UserApp userApp, List<string> audiences)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim(ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return claims;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,client.Id.ToString())
            };

            claims.AddRange(client.Audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));
            return claims;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurity(_customTokenOption.SecurityKey);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _customTokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _customTokenOption.Audience),
                signingCredentials: signingCredentials);

            var jwtHandler = new JwtSecurityTokenHandler();
            var token = jwtHandler.WriteToken(jwtSecurityToken);
            var tokenDto = new TokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration,
                RefreshToken = CreateRefreshToken(),
                RefreshTokenExpiration = refreshTokenExpiration
            };
            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_customTokenOption.AccessTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurity(_customTokenOption.SecurityKey);
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _customTokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials);

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            var clientTokenDto = new ClientTokenDto
            {
                AccessToken = token,
                AccessTokenExpiration = accessTokenExpiration
            };

            return clientTokenDto;
        }
    }
}
