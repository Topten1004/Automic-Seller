using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales.AtomicSeller.Identity
{
    public class User
    {
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public RoleName RoleName { get; set; }
        public List<Claim> Claims { get; set; } = new List<Claim>();
        public List<string> Roles { get; set; } = new List<string>();
        public User()
        {

        }
        public static implicit operator User(ApplicationUser applicationUser)
        {
            User user = new User();
            user.Id = applicationUser.Id;
            user.FirstName = applicationUser.FirstName;
            user.LastName = applicationUser.LastName;
            user.Email = applicationUser.Email;
            user.Username = applicationUser.UserName;
            user.RoleName = RoleName.User;
            return user;
        }
    }
}






