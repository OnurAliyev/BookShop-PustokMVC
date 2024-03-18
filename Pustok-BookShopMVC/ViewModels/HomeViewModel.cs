using Pustok_BookShopMVC.Models;

namespace Pustok_BookShopMVC.ViewModels;

public class HomeViewModel
{
    public List<Slider> Sliders { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Author> Authors { get; set; }
    public List<Book> Books { get; set; }
    public List<BookImage> BookImages { get; set; }
}
