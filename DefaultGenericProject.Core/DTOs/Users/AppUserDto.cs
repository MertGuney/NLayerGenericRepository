using System;

namespace DefaultGenericProject.Core.DTOs.Users
{
    public class AppUserDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime? BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IpAddress { get; set; }
    }
}