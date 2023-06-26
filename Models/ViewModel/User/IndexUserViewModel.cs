using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminMNS.WebApp.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace AdminMNS.WebApp.Models.ViewModel.User
{
	public class IndexUserViewModel
	{
		public string Id { get; set; } = null!;

        [Display(Name = "Prénom")]
		public string? Firstname { get; set; }

		[Display(Name = "Nom")]
		public string? Lastname { get; set; }

        [Display(Name = "Statut")]
        public string RoleNames { get; set; } = null!;

        [Display(Name = "Validation valide")]
        public bool? IsValidRegistration { get; set; }

        [Display(Name = "Formation")]
		public string GraduatingClassName { get; set; }

        public IndexUserViewModel(Data.Entities.User user, IList<string> roles)
        {
            Id = user.Id;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            IsValidRegistration = user.IsValidRegistration;
            GraduatingClassName = user.GraduatingClass?.Name ?? "/";
            RoleNames = string.Join(", ", roles);
        }
    }
}
