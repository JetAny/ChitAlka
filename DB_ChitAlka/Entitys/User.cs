using Microsoft.AspNetCore.Identity;

namespace DB_ChitAlka.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AppUser class
public class User : IdentityUser
{
    public string? NickName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    //public string? Role { get; set; }
    public List< Userlibrary>? Userlibrary { get; set; }
}

