using System.ComponentModel.DataAnnotations;

namespace Pustok_BookShopMVC.Models;

public class Genre:BaseEntity
{
    [StringLength(50)]
    public string Name { get; set; }

    public List<Book>? Books { get; set; }
}
