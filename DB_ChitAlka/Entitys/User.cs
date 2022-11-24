using DB_ChitAlka.Entitys;
using Microsoft.AspNetCore.Identity;

namespace DB_ChitAlka
{
    public partial class User: IdentityUser<Guid>
    {

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role? RoleId { get; set; }
        public string? NickName { get; set; }

        
    }
}
