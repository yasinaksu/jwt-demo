using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Core.Models
{
    public class UserApp : IdentityUser
    {
        public string City { get; set; }
    }
}
