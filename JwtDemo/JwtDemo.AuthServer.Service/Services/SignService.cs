using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Service.Services
{
    public static class SignService
    {
        public static SecurityKey GetSymmetricSecurity(string securityKey)
        {
            var encodedSecurityKey = Encoding.UTF8.GetBytes(securityKey);
            var symmetricSecurityKey = new SymmetricSecurityKey(encodedSecurityKey);
            return symmetricSecurityKey;
        }
    }
}
