using AdminMNS.WebApp.Models.ViewModel.Account;
using AdminMNS.WebApp.Models.ViewModel.User;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminMNS.WebApp.Data.Entities
{
    public class User : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string? WayNumber { get; set; }
        public string? WayType { get; set; }
        public string? WayName { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public bool? IsValidRegistration { get; set; }
        public int? GraduatingClassId { get; set; }
        public GraduatingClass? GraduatingClass { get; set; }

        public User()
        {

        }

        public User(RegisterViewModel model)
        {
            Firstname = model.Firstname;
            Lastname = model.Lastname;
            UserName = $"{model.Firstname}_{model.Lastname}";
            Email = model.Email;
            Birthday = model.Birthday;
            WayNumber = model.WayNumber;
            WayType = model.WayType;
            WayName = model.WayName;
            City = model.City;
            PostalCode = model.PostalCode;
        }

		public User(CreateUserViewModel model)
		{
			Firstname = model.Firstname;
			Lastname = model.Lastname;
			UserName = $"{model.Firstname}_{model.Lastname}";
			Email = model.Email;
			Birthday = model.Birthday;
			WayNumber = model.WayNumber;
			WayType = model.WayType;
            WayName = model.WayName;
			City = model.City;
			PostalCode = model.PostalCode;
            GraduatingClassId = model.GraduatingClassId;
		}
	}
}
