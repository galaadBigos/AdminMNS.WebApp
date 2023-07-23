using AdminMNS.WebApp.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace AdminMNS.WebApp.Models.ViewModel.User
{
	public class DetailsUserViewModel
	{
        public string? Id { get; set; }

        [Display(Name = "Prénom")]
		public string? Firstname { get; set; }

		[Display(Name = "Nom")]
		public string? Lastname { get; set; }

		[Display(Name = "Email")]
		public string? Email { get; set; }

		[Display(Name = "Date de naissance"), DataType(DataType.Date)]
		public DateTime Birthday { get; set; }

		[Display(Name = "Numéro de rue")]
		public string? WayNumber { get; set; }

		[Display(Name = "Voirie")]
		public string? WayType { get; set; }

		[Display(Name = "Nom de rue")]
		public string? WayName { get; set; }

		[Display(Name = "Ville")]
		public string? City { get; set; }

		[Display(Name = "Code postal")]
		public string? PostalCode { get; set; }

		[Display(Name = "Promotion")]
		public string? GraduatingClassName { get; set; }

		[Display(Name = "Statut")]
		public string RoleNames { get; set; } = null!;

        public DetailsUserViewModel()
        {
            
        }

        public DetailsUserViewModel(Data.Entities.User user, IList<string> roleNames)
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
			GraduatingClassName = user.GraduatingClass?.Name ?? "/";
			RoleNames = string.Join(", ", roleNames);
		}
    }
}
