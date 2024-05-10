using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GISApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string RoleId { get; set; }
        public string Address { get; set; }       
        public string RoleName { get; set; }       
        public string? DisplayUserName { get; set; }
        public string? Department { get; set; }
        public string? Alias { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Apartment { get; set;}
        public string? Zip { get; set; }
    }
}
