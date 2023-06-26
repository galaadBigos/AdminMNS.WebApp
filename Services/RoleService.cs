using AdminMNS.WebApp.App_Code.Helpers;
using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Options;
using AdminMNS.WebApp.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AdminMNS.WebApp.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

		public RoleService(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
		}

        public async Task<bool> IsAuthorizedToAdministration(User? user)
        {
            if (user == null)
            {
                return false;
            }
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            return userRoles.Any(ur => Roles.GetAuthorizedRoles().Contains(ur));
        }

        public async Task<bool> HasRole(User? user)
        {
            if (user == null)
            {
                return false;
            }
            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<string?> roles = _roleManager.Roles.ToList().Select(ir => ir.Name).ToList();

            return userRoles.Any(roles.Contains);
        }

        public async Task<string?> GetRoleNameById(string id)
        {
            IdentityRole? role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return null;
            }
            return role.Name;
        }
    }
}
