using Pustok_BookShopMVC.Models;
using System.Linq.Expressions;

namespace Pustok_BookShopMVC.Business.Interfaces;

public interface IBookServicecs
{
    Task<Book> GetByIdAsync(int id);
    Task<Book> GetSingleAsync(Expression<Func<Book, bool>>? expression = null);
    Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>>? expression = null, params string[] includes);
    Task CreateAsync(Book book);
    Task UpdateAsync(Book book);
    Task DeleteAsync(int id);
    Task SoftDeleteAsync(int id);
}
