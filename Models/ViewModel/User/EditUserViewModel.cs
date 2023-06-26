using System.ComponentModel.DataAnnotations;

namespace AdminMNS.WebApp.Models.ViewModel.User
{
	public class EditUserViewModel
	{
		[Required]
		public string? Id { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire"), Display(Name = "Prénom")]
		public string? Firstname { get; set; }

		[Required(ErrorMessage = "Le nom est obligatoire"), Display(Name = "Nom")]
		public string? Lastname { get; set; }

		[Required(ErrorMessage = "L'email est obligatoire"), EmailAddress, Display(Name = "Email")]
		public string? Email { get; set; }

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

		[Display(Name = "Promotion")]
		public int? GraduatingClassId { get; set; }

		[Required(ErrorMessage = "Le statut est obligatoire"), Display(Name = "Statut")]
		public string RoleId { get; set; } = null!;

        public EditUserViewModel()
        {
            
        }

        public EditUserViewModel(Data.Entities.User user)
        {
            Id = user.Id;
			Firstname = user.Firstname;
			Lastname = user.Lastname;
			Email = user.Email;
			Birthday = user.Birthday;
			WayNumber = user.WayNumber;
			WayType = user.WayType;
			WayName = user.WayName;
			City = user.City;
			PostalCode = user.PostalCode;
			GraduatingClassId = user.GraduatingClassId;
        }
    }
}
