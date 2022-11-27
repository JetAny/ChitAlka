using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB_ChitAlka.Areas.Identity.Data;

public class MyDbContext : IdentityDbContext<User>
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());
    }
    public virtual DbSet<Author> Authors { get; set; } = null!;
    public virtual DbSet<Book> Books { get; set; } = null!;
    public virtual DbSet<Section> Sections { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Userlibrary> Userlibraries { get; set; } = null!;
}

public class ApplicationUserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.NickName).HasMaxLength(128);
        builder.Property(u => u.FirstName).HasMaxLength(128);
        builder.Property(u => u.LastName).HasMaxLength(128);
        //builder.Property(u => u.Role).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(128);

}


}