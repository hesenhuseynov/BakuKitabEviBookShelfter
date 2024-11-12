using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.User;
using BookShelfter.Domain.Entities.Identity;

namespace BookShelfter.Application.Abstractions.Services
{
    public  interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser createuserDto);
        Task UpdateRefreshTokenAsync(string refreshToken,AppUser user,DateTime accessTokenDate,int addOnAccessTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<ListUser>> GetAllUserAsync(int page, int size);
        int TotalCounts { get;  }

        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesToUserAsync(string userIdOrName);
        Task<bool> HashRolePermissonToEndpointAsync(string name, string code);

    }
}
