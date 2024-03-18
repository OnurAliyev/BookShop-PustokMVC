using Microsoft.AspNetCore.Mvc;
using Pustok_BookShopMVC.Business.Interfaces;
using Pustok_BookShopMVC.CustomExceptions.SliderExceptions;
using Pustok_BookShopMVC.DataAccesLayer;
using Pustok_BookShopMVC.Extentions;
using Pustok_BookShopMVC.Models;
namespace Pustok_BookShopMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class SliderController : Controller
{
    private readonly Pustok_BookDB _context;
    private readonly ISliderService _sliderService;
    private readonly IWebHostEnvironment _env;

    public SliderController(Pustok_BookDB contex, ISliderService sliderService, IWebHostEnvironment env)
    {
        _context = contex;
        _sliderService = sliderService;
        _env = env;
    }
    public async Task<IActionResult> Index()
        => View(await _sliderService.GetAllAsync(s => s.IsDeleted == false));
    public IActionResult Create()
        => View();
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Slider slider)
    {
        if (!ModelState.IsValid) return View();
        // Image Content Type
        if (slider.ImageFile is not null)
        {
            if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "Content type must be png or jpeg!");
                return View();
            }

            // Image size
            if (slider.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Size must be lower than 2mb!");
                return View();
            }

            //slider.ImageUrl = FileManager.SaveFile(_env.WebRootPath, "uploads/sliders", slider.ImageFile);
            slider.ImageUrl = slider.ImageFile.SaveFile(_env.WebRootPath, "uploads/sliders");
        }
        else
        {
            ModelState.AddModelError("ImageFile", "Image is required!");
            return View();
        }

        slider.CreatedDate = DateTime.UtcNow.AddHours(4);
        slider.ModifiedDate = DateTime.UtcNow.AddHours(4);
        await _sliderService.CreateAsync(slider);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        var existSlider = await _context.Sliders.FindAsync(id);
        if (existSlider is null) throw new SliderNotFoundException("Slider not found!");

        return View(existSlider);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Slider slider)
    {
        if (!ModelState.IsValid) return View();
        var existSlider = await _context.Sliders.FindAsync(slider.Id);
        if (existSlider is null) throw new SliderNotFoundException("Slider not found!");

        if (slider.ImageFile is not null)
        {
            if (slider.ImageFile.ContentType != "image/jpeg" && slider.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "Content type must be png or jpeg!");
                return View();
            }

            // Image size
            if (slider.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Size must be lower than 2mb!");
                return View();
            }

            FileManager.DeleteFile(_env.WebRootPath, "uploads/sliders", existSlider.ImageUrl);

            existSlider.ImageUrl = slider.ImageFile.SaveFile(_env.WebRootPath, "uploads/sliders");
        }
        await _sliderService.UpdateAsync(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        var existSlider = await _context.Sliders.FindAsync(id);
        if (existSlider is null) return NotFound();
        await _sliderService.DeleteAsync(id);
        return Ok();
    }
}
