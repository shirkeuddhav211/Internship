using System.ComponentModel.DataAnnotations;
namespace GISApi.Models
{

   public class AddEditUserViewModel
    {
        public string Id { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string? UserName { get; set; }
        [StringLength(100, ErrorMessage = "UserId Max Length is 100")]

        public string? Email { get; set; }

        [MaxLength(30)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }
        public string Address { get; set; }

        public string Password { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string DisplayUserName { get; set; }
        
        public string? Department { get; set; }       
        public string? Alias { get; set; }        
        public string? UpdatedBy { get; set; }        
        public DateTime? UpdatedDate { get; set; }        
        public string? AddressLine1 { get; set; }        
        public string? AddressLine2 { get; set; }        
        public string? City { get; set; }
        public string newPassword { get; set; }
        public string Name { get; set; }
        public string? State { get; set; } 
        public string? Apartment { get; set; }
        public string? Zip { get; set; }
    }

    public class ManagersFroDDL
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
