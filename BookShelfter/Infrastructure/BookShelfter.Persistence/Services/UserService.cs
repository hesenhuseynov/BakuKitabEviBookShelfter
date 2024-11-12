using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Abstractions.Token;
using BookShelfter.Application.DTOs.User;
using BookShelfter.Domain.Entities.Identity;
using BookShelfter.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly BookShelfterDbContext _context;



        public UserService(UserManager<AppUser> userManager,  ITokenService tokenService, BookShelfterDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = model.Username,
                    Email = model.Email,
                    NameSurName = model.NameSurname
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    return new CreateUserResponse
                    {
                        Succeeded = false,
                        Message = string.Join("\n", result.Errors.Select(e => $"{e.Code}-{e.Description}"))
                    };
                }

                var token = _tokenService.CreateAccessToken(1, user, await _userManager.GetRolesAsync(user));
                if (token == null)
                {
                    await transaction.RollbackAsync();
                    return new CreateUserResponse
                    {
                        Succeeded = false,
                        Message = "Token could not be created"
                    };
                }

                await transaction.CommitAsync();
                return new CreateUserResponse
                {
                    Succeeded = true,
                    Message = "User created successfully",
                    Token = token
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new CreateUserResponse
                {
                    Succeeded = false,
                    Message = $"An error occurred during user creation: {ex.Message}"
                };
            }
        }

        public Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {   

            throw new NotImplementedException();
        }

        public async Task<List<ListUser>> GetAllUserAsync(int page, int size)
        {
            var users = await _userManager.Users
                .Skip(page * size)
                .Take(size)
                .ToListAsync();

            return users.Select(user => new ListUser
            {
                Id = user.Id,
                Email = user.Email,
                NameSurname = user.NameSurName,
                TwoFactorEnabled = user.TwoFactorEnabled,
                UserName = user.UserName

            }).ToList();
        }

        public int TotalCounts { get; }
        public Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> GetRolesToUserAsync(string userIdOrName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HashRolePermissonToEndpointAsync(string name, string code)
        {
            throw new NotImplementedException();
        }
    }
}
