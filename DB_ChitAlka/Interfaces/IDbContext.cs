using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_ChitAlka.Interfases
{
    public interface IDbContext
    {
        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Capter> Capters { get; set; }
        DbSet<User> Users { get; set; } 
        DbSet<Userlibrary> Userlibraries { get; set; } 

        int SaveChanges();
    }
}
