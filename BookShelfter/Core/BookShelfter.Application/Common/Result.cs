using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Common
{
    public  class Result:IResult
    {
        public bool Succes { get; }
        public string Message { get; }
        public List<string> Errors { get; }
        public string RedirectUrl { get; set; }

        public Result(bool success, string message, List<string> errors,string redirectUrl= null)
        {
            Succes = success;
            Message = message;
            Errors = errors;
                RedirectUrl = redirectUrl;
        }

        public Result(bool success, string message,string redirectUrl=null)
        {
            Succes = success;
            Message = message;
            Errors = new List<string>();
            RedirectUrl = redirectUrl;
        }

        public static IResult Redirect(string url)
        {
            return new Result(true, "Redirecting...", url);
        }
    }
}
