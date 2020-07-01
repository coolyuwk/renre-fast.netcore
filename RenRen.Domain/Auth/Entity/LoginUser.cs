using System;
using System.Collections.Generic;
using System.Text;

namespace RenRen.Domain.Auth.Entity
{
    public class LoginUser
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string Salt { get; set; }
    }
}
