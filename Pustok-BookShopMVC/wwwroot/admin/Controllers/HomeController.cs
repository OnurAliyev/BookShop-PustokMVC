using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Business.Interfaces;
using PustokMVC.Data;
using PustokMVC.ViewModels;

namespace PustokMVC.Controllers;

public class HomeController : Controller
{
    private readonly PustokDbContext _context;
    private readonly IGenreService _genreService;

    public HomeController(PustokDbContext context, IGenreService genreService)
    {
        _context = context;
        _genreService = genreService;
    }

    public async Task<IActionResult> Index()
    {
        HomeViewModel homeVM = new HomeViewModel()
        {
            Sliders = await _context.Sliders.ToListAsync(),
            Genres = await _genreService.GetAllAsync(x=>x.IsDeleted == false)
        };
        return View(homeVM);
    }
}
