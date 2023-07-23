using System.ComponentModel.DataAnnotations;

namespace AdminMNS.WebApp.Models.ViewModel.Account
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Veuillez indiquer une adresse mail valide"), EmailAddress]
        public string? Email { get; set; }
    }
}
