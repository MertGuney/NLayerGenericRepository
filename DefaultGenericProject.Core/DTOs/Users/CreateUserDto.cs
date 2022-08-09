using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DefaultGenericProject.Core.DTOs.Users
{
    public class CreateUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}