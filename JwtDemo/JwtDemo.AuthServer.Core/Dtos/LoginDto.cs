﻿using System;
using System.Collections.Generic;
using System.Text;

namespace JwtDemo.AuthServer.Core.Dtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
