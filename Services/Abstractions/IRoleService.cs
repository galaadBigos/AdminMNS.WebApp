using AdminMNS.WebApp.Data.Entities;

namespace AdminMNS.WebApp.Services.Abstractions
{
    public interface IRoleService
    {
        Task<bool> IsAuthorizedToAdministration(User? user);
        Task<bool> HasRole(User? user);
        Task<string?> GetRoleNameById(string id);
	}
}
