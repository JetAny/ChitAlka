
using DB_ChitAlka.Entitys;
using DB_ChitAlka.Interfases;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DB_ChitAlka
{
    public partial class dbchitalcaContext : IdentityDbContext<User, Role, Guid>
    {
        //public dbchitalcaContext()
        //{
        //}
        protected readonly IConfiguration Configuration;
        public dbchitalcaContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public dbchitalcaContext(DbContextOptions<dbchitalcaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Userlibrary> Userlibraries { get; set; } = null!;
    }
}

// //       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
////        {
////            if (!optionsBuilder.IsConfigured)
////            {
//////#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
//////                You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see
//////                    https://go.microsoft.com/fwlink/?linkid=2131148.
////                          //For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
////                optionsBuilder.UseMySql("server=localhost;database=dbchitalca30;uid=root;pwd=Tucha0425#", Microsoft.EntityFrameworkCore. ServerVersion.Parse("8.0.30-mysql"));
////            }
////        }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
//                .HasCharSet("utf8mb4");

//            modelBuilder.Entity<Author>(entity =>
//            {
//                entity.ToTable("author");

//                entity.Property(e => e.FirstName).HasMaxLength(15);

//                entity.Property(e => e.LastName).HasMaxLength(20);
//            });

//            modelBuilder.Entity<Book>(entity =>
//            {
//                entity.ToTable("book");

//                entity.Property(e => e.BookImage).HasMaxLength(100);

//                entity.Property(e => e.Annotation).HasMaxLength(500);

//                entity.Property(e => e.Genre).HasMaxLength(20);

//                entity.Property(e => e.BookTitle).HasMaxLength(100);
//            });

//            modelBuilder.Entity<Section>(entity =>
//            {
//                entity.ToTable("section");

//                entity.Property(e => e.Title)
//                    .HasMaxLength(100)
//                    .HasColumnName("Title");

//                entity.Property(e => e.Text)
//                    .HasMaxLength(20000)
//                    .HasColumnName("Text");
//            });

//            modelBuilder.Entity<User>(entity =>
//            {
//                entity.ToTable("user");

//                //entity.HasIndex(e => new { e.Id, e.Password, e.FirstName, e.LastName, e.NickName, e.Role }, "user_Id_IDX");
//                entity.HasIndex(e => new { e.FirstName, e.LastName}, "user_Id_IDX");

//                entity.Property(e => e.FirstName).HasMaxLength(15);

//                entity.Property(e => e.LastName).HasMaxLength(20);

//                //entity.Property(e => e.NickName).HasMaxLength(10);

//                //entity.Property(e => e.Password).HasMaxLength(10);

//                //entity.Property(e => e.Role).HasMaxLength(10);
//            });

//            modelBuilder.Entity<Userlibrary>(entity =>
//            {
//                entity.ToTable("userlibrary");

              

                

               
//            });

//            OnModelCreatingPartial(modelBuilder);
//        }

//        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//    }
//}
