using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
    }
}
