using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pustok_BookShopMVC.Models;

public class Slider:BaseEntity
{
    [Required]
    [StringLength(20)]
    public string Title1 { get; set; }
    [Required]
    [StringLength(20)]
    public string Title2 { get; set; }
    [Required]
    [StringLength(150)]
    public string Desc { get; set; }
    public string? RedirectUrl { get; set; }
    [Required]
    [StringLength(40)]
    public string RedirectUrlText { get; set; }
    [StringLength(100)]
    public string? ImageUrl { get; set; }
    [NotMapped]
    public IFormFile? ImageFile { get; set; }
}
