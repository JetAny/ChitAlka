using System;
using System.Collections.Generic;

namespace MVC_ChitAlka
{
    public partial class UserModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
