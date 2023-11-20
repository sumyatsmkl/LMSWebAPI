using System.ComponentModel.DataAnnotations;

namespace Domain.Utilities.Request
{
    public class LoginInfo
    {

        [Display(Name = "User Name")]
        [MaxLength(100)]
        [Required(ErrorMessage = "Please enter user name.")]
        public string UserName { get; set; }
        
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter password.")]
        public string Password { get; set; }

    }
}
