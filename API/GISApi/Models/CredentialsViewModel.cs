using System.ComponentModel.DataAnnotations;

namespace GISApi.Models
{
    public class CredentialsViewModel
    {
        //[Required]
        //[MaxLength(255)]
        //[EmailAddress]
       // [RegularExpression("^[^{}<>|#*&-?/!]*$", ErrorMessage  = "Special Character are not allowed")]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(30)]
        //[RegularExpression("^[^{}<>|#*&-.?/]*", ErrorMessage = "Special Character are not allowed")]
        public string Password { get; set; }
    }


    public class UserNameViewModel
    {
        public string? UserName { get; set; }

    }
    public class OtpViewModel
    {
        public int Otp { get; set; }
    }


    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old Password")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        //[Required]
        //[Display(Name = "Email")]
        //[EmailAddress]
        //public string Email { get; set; }

        public string UserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Code { get; set; }

        //public string ActionType { get; set; }
    }
}
