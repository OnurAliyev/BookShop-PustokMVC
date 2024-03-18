using Microsoft.EntityFrameworkCore;
using Pustok_BookShopMVC.Business.Implementations;
using Pustok_BookShopMVC.Business.Interfaces;
using Pustok_BookShopMVC.DataAccesLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<Pustok_BookDB>(opt => 
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("default"));
}
);
builder.Services.AddScoped<IGenreService,GenreService>();
builder.Services.AddScoped<IBookServicecs, BookService>();
builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<ISliderService, SliderService>();

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
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
