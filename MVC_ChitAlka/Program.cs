using DB_ChitAlka;
using DB_ChitAlka.Interfases;
using MVC_ChitAlka.Intrfaces;
using MVC_ChitAlka.Servises;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IDbContext, dbchitalcaContext>();

builder.Services.AddScoped<IGetAllBooksService, GetAllBooksService>();
builder.Services.AddScoped<IGetBookService, GetBookService>();
builder.Services.AddScoped<IGetSectionBookService, GetSectionBookService>();
builder.Services.AddScoped< IAddBookService, AddBookService>();
builder.Services.AddScoped<ISearchBookService, SearchBookService>();



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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
