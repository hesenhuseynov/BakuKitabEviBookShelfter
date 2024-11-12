using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BookShelfter.Application.Features.Queries.User
{
    public  class GetAllUsersQueryRequest:IRequest<GetAllUsersQueryResponse>
    {
    }
}
