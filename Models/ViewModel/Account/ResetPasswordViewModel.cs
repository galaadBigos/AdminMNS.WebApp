using System.ComponentModel.DataAnnotations;

namespace AdminMNS.WebApp.Models.ViewModel.Account
{
    public class ResetPasswordViewModel
    {
        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire"), DataType(DataType.Password), Display(Name = "Mot de passe")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Le confirmation du mot de passe est obligatoire"), DataType(DataType.Password), Display(Name = "Confirmation du mot de passe")]
        public string? ConfirmPassword { get; set; }

        [Required]
        public string? Code { get; set; }
    }
}
