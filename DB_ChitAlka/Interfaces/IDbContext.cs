using DB_ChitAlka.Entitys;
using Microsoft.EntityFrameworkCore;


namespace DB_ChitAlka.Interfases
{
    public interface IDbContext
    {
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Section> Sections { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Userlibrary> Userlibraries { get; set; } 

        int SaveChanges();
    }
}
