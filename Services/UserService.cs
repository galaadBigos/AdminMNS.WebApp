using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Models.ViewModel.User;
using AdminMNS.WebApp.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AdminMNS.WebApp.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<List<IndexUserViewModel>> CreateIndexUserViewModels()
        {
			List<User> users = await _userManager.Users.Include(u => u.GraduatingClass).ToListAsync();
			List<IndexUserViewModel> result = new List<IndexUserViewModel>();

			foreach (User user in users)
			{
				IList<string> roleNames = await _userManager.GetRolesAsync(user);
				IndexUserViewModel modelUser = new IndexUserViewModel(user, roleNames);

				result.Add(modelUser);
			}
			return result;
		}

		public async Task<IdentityResult> AddUserInDatabase(CreateUserViewModel model, User user)
		{
			IdentityResult result = await _userManager.CreateAsync(user, model.Password);
			IdentityRole? roleUser = await _roleManager.FindByIdAsync(model.RoleId);

			if (roleUser?.Name != null)
			{
				await _userManager.AddToRoleAsync(user, roleUser.Name);
			}
			return result;
		}

		public async Task<IdentityResult> UpdateUser(string id, EditUserViewModel model)
		{
			IdentityResult result = IdentityResult.Failed();
			User? user = await _userManager.FindByIdAsync(id);

			if (user != null)
			{
				user = AddModelPropertiesToUser(user, model);
				await AddNewRoleToUser(user, model.RoleId);

				result = await _userManager.UpdateAsync(user);
			}
			return result;
		}

		private User AddModelPropertiesToUser(User user, EditUserViewModel model)
		{
			user.Firstname = model.Firstname;
			user.Lastname = model.Lastname;
			user.UserName = $"{model.Firstname}_{model.Lastname}";
			user.Email = model.Email;
			user.Birthday = model.Birthday;
			user.WayNumber = model.WayNumber;
			user.WayType = model.WayType;
			user.WayName = model.WayName;
			user.City = model.City;
			user.PostalCode = model.PostalCode;
			user.GraduatingClassId = model.GraduatingClassId;

			return user;
		}

		private async Task AddNewRoleToUser(User user, string roleId)
		{
			IdentityRole? newUserRole = await _roleManager.FindByIdAsync(roleId);

			if (newUserRole?.Name != null)
			{
				IList<string> userRoles = await _userManager.GetRolesAsync(user);
				await _userManager.RemoveFromRolesAsync(user, userRoles);
				await _userManager.AddToRoleAsync(user, newUserRole.Name);

			}
		}
	}
}
