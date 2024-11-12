using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Common
{
    public  interface IResult
    {
         bool Succes { get;  }
         string Message { get; }
         List<string> Errors { get; }

    }
}
