using System;
using System.Collections.Generic;

namespace DB_ChitAlka
{
    public partial class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string LastName { get; set; } = null!;
        public string? NickName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }
}
