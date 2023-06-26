namespace AdminMNS.WebApp.App_Code.Helpers
{
	public static class Roles
	{
		public const string Admin = "Admin";
		public const string Personnel = "Personnel";
		public const string Intern = "Intern";
		public const string Candidate = "Candidate";
		public const string User = "User";

		public static string[] GetAuthorizedRoles()
		{
			return new string[]
			{
				Admin,
				Personnel,
			};
		}
	}
}
