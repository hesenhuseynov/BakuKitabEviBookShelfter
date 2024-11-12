using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.DTOs.User;
using BookShelfter.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Application.Features.Queries.User
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {

        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public GetAllUsersQueryHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);



            if (users == null || users.Count == 0)
            {

                return new()
                {
                    Sucess = false,
                    Message = "Not users found"
                };


            }

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
                NameSurName = user.NameSurName
            }).ToList();
             
            

            return new()
            {
                Sucess = true,
                Users = userDtos
            };




            
        }
    }
}
