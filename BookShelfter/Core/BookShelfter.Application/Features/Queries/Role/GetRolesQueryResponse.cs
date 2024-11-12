using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Common;
using BookShelfter.Domain.Entities.Identity;
using MediatR;

namespace BookShelfter.Application.Features.Queries.Role
{
    public  class GetRolesQueryResponse : IRequest<GetRolesQueryRequest>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<AppRole> Roles { get; set; }
    }
}
