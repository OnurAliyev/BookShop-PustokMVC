namespace Pustok_BookShopMVC.CustomExceptions.BookExceptions;

public class BookNotFoundException:Exception
{
    public BookNotFoundException() { }
    public BookNotFoundException(string? message) : base(message) { }
}
