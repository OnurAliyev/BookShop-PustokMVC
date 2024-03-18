using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_BookShopMVC.Business.Interfaces;
using Pustok_BookShopMVC.DataAccesLayer;
using Pustok_BookShopMVC.ViewModels;
namespace Pustok_BookShopMVC.Controllers;
public class HomeController : Controller
{
    private readonly ISliderService _sliderservice;
    private readonly IGenreService _genreService;
    private readonly IAuthorService _authorService;
    private readonly IBookServicecs _bookServicecs;

    public HomeController(IGenreService genreService, ISliderService sliderservice, IAuthorService authorService, IBookServicecs bookServicecs)
    {
        _genreService = genreService;
        _sliderservice = sliderservice;
        _authorService = authorService;
        _bookServicecs = bookServicecs;
    }
    public async Task<IActionResult> Index()
    {
        HomeViewModel viewModel= new HomeViewModel() 
        {
            Sliders= await _sliderservice.GetAllAsync(s=>s.IsDeleted==false),
            Genres = await _genreService.GetAllAsync(g=>g.IsDeleted==false),
            Authors = await _authorService.GetAllAsync(a=>a.IsDeleted==false),
            Books = await _bookServicecs.GetAllAsync(b=>b.IsDeleted==false,"Author","Genre","BookImages"),
        };
        return View(viewModel);
    }
}
