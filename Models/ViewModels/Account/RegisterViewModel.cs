using System.ComponentModel.DataAnnotations;

namespace AdminMNS.WebApp.Models.ViewModel.Account
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Le prénom est obligatoire"), Display(Name = "Prénom")]
		public string? Firstname { get; set; }

		[Required(ErrorMessage = "Le nom est obligatoire"), Display(Name = "Nom")]
		public string? Lastname { get; set; }

		[Required(ErrorMessage = "L'email est obligatoire"), EmailAddress, Display(Name = "Email")]
		public string? Email { get; set; }

		[Required(ErrorMessage = "Le mot de passe est obligatoire"), DataType(DataType.Password), Display(Name = "Mot de passe")]
		public string? Password { get; set; }

		[Required(ErrorMessage = "Le confirmation du mot de passe est obligatoire"), DataType(DataType.Password), Display(Name = "Confirmation du mot de passe")]
		public string? ConfirmPassword { get; set; }

		[Required(ErrorMessage = "La date de naissance est obligatoire"), DataType(DataType.Date), Display(Name = "Date de naissance")]
		public DateTime Birthday { get; set; }

		[Required(ErrorMessage = "Le numéro de rue est obligatoire"), Display(Name = "Numéro de rue")]
		public string? WayNumber { get; set; }

		[Required(ErrorMessage = "La voirie est obligatoire"), Display(Name = "Voirie")]
		public string? WayType { get; set; }

		[Required(ErrorMessage = "Le nom de rue est obligatoire"), Display(Name = "Nom de rue")]
		public string? WayName { get; set; }

		[Required(ErrorMessage = "La ville est obligatoire"), Display(Name = "Ville")]
		public string? City { get; set; }

		[Required(ErrorMessage = "Le code postal est obligatoire"), Display(Name = "Code postal")]
		public string? PostalCode { get; set; }
	}
}
