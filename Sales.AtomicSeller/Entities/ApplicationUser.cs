using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.AtomicSeller.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string? Company { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        [Display(Name = "Street Address 1")]
        public string? StreetAddress1 { get; set; }

        [Display(Name = "Street Address 2")]
        public string? StreetAddress2 { get; set; }

        [Display(Name = "Post Code Zip")]
        public string? PostCodeZip { get; set; }

        public string? City { get; set; }

    }
}
