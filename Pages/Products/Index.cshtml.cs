using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Products; // produkty

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public List<Product> Products { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
    public string Search { get; set; }
    public string SortBy { get; set; }
    public bool Desc { get; set; }

    public IndexModel(Db db) => _db = db;

    public void OnGet(string search, string sortBy, bool desc)
    {
        Search = search;
        SortBy = sortBy ?? "name";
        Desc = desc;
        var data = _db.Load();
        Categories = data.Categories;

        IEnumerable<Product> prods = string.IsNullOrWhiteSpace(search)
            ? data.Products
            : data.Products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase)
                                    || p.Sku.Contains(search, StringComparison.OrdinalIgnoreCase));

        // sortowanie po wybranej kolumnie
        if (SortBy == "quantity")
            prods = Desc ? prods.OrderByDescending(p => p.Quantity) : prods.OrderBy(p => p.Quantity);
        else if (SortBy == "price")
            prods = Desc ? prods.OrderByDescending(p => p.Price) : prods.OrderBy(p => p.Price);
        else if (SortBy == "category")
            prods = Desc ? prods.OrderByDescending(p => p.CategoryId ?? 0) : prods.OrderBy(p => p.CategoryId ?? 0);
        else
            prods = Desc ? prods.OrderByDescending(p => p.Name) : prods.OrderBy(p => p.Name);

        Products = prods.ToList();
    }
}
