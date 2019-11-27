using System.Collections.Generic;
using Domain.Entities;

namespace WebUI.Models
{
    public class UserViewModel
    {
        public IEnumerable<User> Users { get; set; }

        public bool UserIsMe { get; set; }
    }
}



