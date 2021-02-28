using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Core.Dtos
{
    public class ClientLoginDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

    }
}
