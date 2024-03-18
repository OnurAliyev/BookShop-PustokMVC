using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_BookShopMVC.Business.Interfaces;
using Pustok_BookShopMVC.CustomExceptions.BookExceptions;
using Pustok_BookShopMVC.DataAccesLayer;
using Pustok_BookShopMVC.Extentions;
using Pustok_BookShopMVC.Models;
namespace Pustok_BookShopMVC.Areas.Admin.Controllers;
[Area("Admin")]
public class BookController : Controller
{
    private readonly Pustok_BookDB _context;
    private readonly IWebHostEnvironment _env;
    private readonly IBookServicecs _bookServicecs;

    public BookController(Pustok_BookDB context , IWebHostEnvironment env,IBookServicecs bookServicecs)
    {
        _context = context;
        _env = env;
        _bookServicecs = bookServicecs;
    }
    public async Task<IActionResult> Index()
        => View(await _context.Books.Include("Author")
            .Include("Genre")
            .Include("BookImages")
            .ToListAsync());
    public async Task<IActionResult> Create()
    {
        ViewBag.Genres = await _context.Genres.ToListAsync();
        ViewBag.Authors = await _context.Authors.ToListAsync();

        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        ViewBag.Genres = await _context.Genres.ToListAsync();
        ViewBag.Authors = await _context.Authors.ToListAsync();
        if (!ModelState.IsValid) return View();
        if (book.PosterImageFile.ContentType != "image/jpeg" && book.PosterImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("PosterImageFile", "Content type must be png or jpeg!");
            return View();
        }

        if (book.PosterImageFile.Length > 2097152)
        {
            ModelState.AddModelError("PosterImageFile", "Size must be lower than 2mb!");
            return View();
        }
        if (book.HoverImageFile.ContentType != "image/jpeg" && book.HoverImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("HoverImageFile", "Content type must be png or jpeg!");
            return View();
        }

        if (book.HoverImageFile.Length > 2097152)
        {
            ModelState.AddModelError("HoverImageFile", "Size must be lower than 2mb!");
            return View();
        }

        if (book.ImageFiles is not null)
        {
            foreach (var imageFile in book.ImageFiles)
            {
                if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFiles", "Content type must be png or jpeg!");
                    return View();
                }

                if (imageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFiles", "Size must be lower than 2mb!");
                    return View();
                }
                BookImage bookImage = new BookImage()
                {
                    Book = book,
                    ImageUrl = imageFile.SaveFile(_env.WebRootPath, "uploads/books"),
                    IsPoster = null
                };
                await _context.BookImages.AddAsync(bookImage);
            }
        }
        await _bookServicecs.CreateAsync(book);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Update(int id)
    {
        ViewBag.Genres = await _context.Genres.ToListAsync();
        ViewBag.Authors = await _context.Authors.ToListAsync();
        var existBook = await _context.Books.FindAsync(id);
        if (existBook is null) throw new BookNotFoundException("Book not found!");
        return View(existBook);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(Book book)
    {
        ViewBag.Genres = await _context.Genres.ToListAsync();
        ViewBag.Authors = await _context.Authors.ToListAsync();
        if (!ModelState.IsValid) return View();
        if (book.PosterImageFile.ContentType != "image/jpeg" && book.PosterImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("PosterImageFile", "Content type must be png or jpeg!");
            return View();
        }

        if (book.PosterImageFile.Length > 2097152)
        {
            ModelState.AddModelError("PosterImageFile", "Size must be lower than 2mb!");
            return View();
        }
        if (book.HoverImageFile.ContentType != "image/jpeg" && book.HoverImageFile.ContentType != "image/png")
        {
            ModelState.AddModelError("HoverImageFile", "Content type must be png or jpeg!");
            return View();
        }

        if (book.HoverImageFile.Length > 2097152)
        {
            ModelState.AddModelError("HoverImageFile", "Size must be lower than 2mb!");
            return View();
        }

        if (book.ImageFiles is not null)
        {
            foreach (var imageFile in book.ImageFiles)
            {
                if (imageFile.ContentType != "image/jpeg" && imageFile.ContentType != "image/png")
                {
                    ModelState.AddModelError("ImageFiles", "Content type must be png or jpeg!");
                    return View();
                }

                if (imageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFiles", "Size must be lower than 2mb!");
                    return View();
                }
                BookImage bookImage = new BookImage()
                {
                    Book = book,
                    ImageUrl = imageFile.SaveFile(_env.WebRootPath, "uploads/books"),
                    IsPoster = null
                };
                await _context.BookImages.AddAsync(bookImage);
            }
        }
        await _bookServicecs.UpdateAsync(book);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Delete(int id)
    {
        var existBook = await _context.Books.FindAsync(id);
        if(existBook is null) return NotFound();
        await _bookServicecs.DeleteAsync(id);
        return Ok();

    }
}
