using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Products;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public List<Product> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public string Search { get; set; }

    public IndexModel(Db db) => _db = db;

    public void OnGet(string search)
    {
        Search = search;
        var data = _db.Load();
        Categories = data.Categories;

        Products = string.IsNullOrWhiteSpace(search)
            ? data.Products
            : data.Products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                                    || p.Sku.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
