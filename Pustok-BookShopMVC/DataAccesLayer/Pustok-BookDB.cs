using Microsoft.EntityFrameworkCore;
using Pustok_BookShopMVC.Models;

namespace Pustok_BookShopMVC.DataAccesLayer;

public class Pustok_BookDB : DbContext
{
    public Pustok_BookDB(DbContextOptions<Pustok_BookDB> options) : base(options) { }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookImage> BookImages { get; set; }
}
