
using DB_ChitAlka.Areas.Identity.Data;
using MVC_ChitAlka.Intrfaces;
using MVC_ChitAlka.Servises;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGetAllBooksService, GetAllBooksService>();
builder.Services.AddScoped<IGetBookService, GetBookService>();
builder.Services.AddScoped<IGetSectionBookService, GetSectionBookService>();
builder.Services.AddScoped<IAddBookService, AddBookService>();
builder.Services.AddScoped<ISearchBookService, SearchBookService>();
builder.Services.AddScoped<IDeleteBookService, DeleteBookService>();
builder.Services.AddScoped<IUserLibraryService, UserLibraryService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))); //для MySQL
//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseSqlServer(connectionString)); // для SQL

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false) // false для отмены потверждения регистрации
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<MyDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole",
         policy => policy.RequireRole("SuperAdmin","Admin"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); //при работе с RazorPages (может иногда без объяления работать, но не стабильно)

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages(); //при работе с RazorPages (в шаблон MVC нет)
app.Run();
