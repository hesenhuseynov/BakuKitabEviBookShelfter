using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Common
{    public interface  IDataResult<T>:IResult
    {
        T Data { get;  } 
    }
}
