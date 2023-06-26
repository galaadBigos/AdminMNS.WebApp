using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Models.ViewModel.User;
using Microsoft.AspNetCore.Identity;

namespace AdminMNS.WebApp.Services.Abstractions
{
	public interface IUserService
	{
		Task<List<IndexUserViewModel>> CreateIndexUserViewModels();
		Task<IdentityResult> AddUserInDatabase(CreateUserViewModel model, User user);
		Task<IdentityResult> UpdateUser(string id, EditUserViewModel model);
	}
}
