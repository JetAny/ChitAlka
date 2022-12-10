
using DB_ChitAlka.Areas.Identity.Data;
using MVC_ChitAlka.Intrfaces;
using MVC_ChitAlka.Servises;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IGetAllBooksService, GetAllBooksService>();
builder.Services.AddScoped<IGetBookService, GetBookService>();
builder.Services.AddScoped<IGetSectionBookService, GetSectionBookService>();
builder.Services.AddScoped<IAddBookService, AddBookService>();
builder.Services.AddScoped<ISearchBookService, SearchBookService>();
builder.Services.AddScoped<IDeleteBookService, DeleteBookService>();
builder.Services.AddScoped<IUserLibraryService, UserLibraryService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<MyDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
app.MapRazorPages();
app.Run();
