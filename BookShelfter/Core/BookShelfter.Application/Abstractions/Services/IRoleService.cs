using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace BookShelfter.Application.Abstractions.Services
{
    public  interface IRoleService
    {
        Task<IdentityResult> CreateRoleAsync(string roleName, string description);
        Task<IdentityResult> DeleteRoleAsync(string roleName);
        Task<IdentityResult> UpdateRoleAsync(string roleName, string newDescription);
        Task<AppRole> GetRoleByNameAsync(string roelName);
        Task<List<AppRole>> GetAllRolesAsync();
        Task<IdentityResult> AssignRoleToUserAsync(string userId, string roleName);
        Task<IdentityResult> RemoveRoleFromUserAsync(string userId, string roleName);
        Task<List<string>> GetUserRolesAsync(string userId);

    }
}
