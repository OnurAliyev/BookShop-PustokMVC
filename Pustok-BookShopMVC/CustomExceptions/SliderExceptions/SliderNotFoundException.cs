namespace Pustok_BookShopMVC.CustomExceptions.SliderExceptions;

public class SliderNotFoundException:Exception
{
    public SliderNotFoundException() { }
    public SliderNotFoundException(string? message) : base(message) { }
}
