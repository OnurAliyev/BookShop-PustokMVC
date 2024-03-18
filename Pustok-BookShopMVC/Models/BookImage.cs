namespace Pustok_BookShopMVC.Models
{
    public class BookImage:BaseEntity
    {
        public int BookId { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsPoster { get; set; } // true Poster False BackPoster null Detail

        public Book Book { get; set; }
    }
}
