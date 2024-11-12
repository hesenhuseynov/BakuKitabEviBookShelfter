using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Services
{
    public  class RoleService:IRoleService
    {
        public readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleService(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async  Task<IdentityResult> CreateRoleAsync(string roleName, string description)
        {
            var role = new AppRole { 
                
                Name = roleName,
                Description = description,
                Id=Guid.NewGuid().ToString()


            };
            return await _roleManager.CreateAsync(role);
        }

        public  async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            return await _roleManager.DeleteAsync(role);
        }

        public async  Task<IdentityResult> UpdateRoleAsync(string roleName, string newDescription)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                role.Description=newDescription;
                return await _roleManager.UpdateAsync(role);

            }

            return IdentityResult.Failed();
        }

        public async  Task<AppRole> GetRoleByNameAsync(string roelName)
        {
            var role = await  _roleManager.FindByNameAsync(roelName);
            return role;
        }

        public async  Task<List<AppRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();

        }

        public async  Task<IdentityResult> AssignRoleToUserAsync(string userId, string roleName)
        {
             var user =await _userManager.FindByIdAsync(userId);
             if (user == null)
             {
                 return IdentityResult.Failed(new IdentityError() { Description = "User not found" });

             }
             return await _userManager.AddToRoleAsync(user, roleName);


        }


        public async  Task<IdentityResult> RemoveRoleFromUserAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await  _userManager.RemoveFromRoleAsync(user, roleName);


              
        }

        public async  Task<List<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByNameAsync(userId);
            return await _userManager.GetRolesAsync(user) as List<string>;
        }
    }
}
