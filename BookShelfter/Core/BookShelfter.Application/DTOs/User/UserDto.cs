using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.DTOs.User
{
    public class UserDto
    {

        public string Id { get; set; }
        public string UserName  { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        //public string NormalizedEmail { get; set; }
        //public bool LockoutEnabled { get; set; }
        public string NameSurName { get; set; }

        //public int AccessFailedCount { get; set; }
    }
}
