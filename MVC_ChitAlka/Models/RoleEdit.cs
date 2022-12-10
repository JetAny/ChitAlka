using DB_ChitAlka.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;


namespace DB_ChitAlka.Entitys
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}
