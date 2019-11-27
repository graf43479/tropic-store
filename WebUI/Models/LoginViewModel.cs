using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Login { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }

        
        [Display(Name = "Докажите, что вы не робот")]
        public string Captcha { get; set; }


    }
}