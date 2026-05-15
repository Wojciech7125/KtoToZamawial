using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Stock;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public List<Product> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

    public IndexModel(Db db) => _db = db;

    public void OnGet()
    {
        // ladujemy wszystkie produkty i kategorie
        var data = _db.Load();
        Products = data.Products;
        Categories = data.Categories;
    }
}
