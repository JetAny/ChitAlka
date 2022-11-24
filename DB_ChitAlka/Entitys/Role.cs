using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_ChitAlka.Entitys
{
    public class Role : IdentityRole<Guid>
    {
        public Guid Id { get; set; }
        public string? RoleName { get; set; }
        public List<User>? UserId { get; set; }
    }
}
