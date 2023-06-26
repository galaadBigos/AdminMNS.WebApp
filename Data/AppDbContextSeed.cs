using Microsoft.AspNetCore.Identity;
using System;

namespace AdminMNS.WebApp.Data
{
    public static class AppDbContextSeed
    {
        private static readonly List<string> _roles = new List<string>
        {
            "User",
            "Admin",
            "Personnel",
            "Intern",
            "Candidate",
        };
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (string role in _roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole { Name = role, ConcurrencyStamp = Guid.NewGuid().ToString() });
            }
        }
    }
}
