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
    public string SortBy { get; set; }
    public bool Desc { get; set; }

    public IndexModel(Db db) => _db = db;

    public void OnGet(string sortBy, bool desc)
    {
        // sortowanie po kolumnie - domyslnie po nazwie rosnaco
        SortBy = sortBy ?? "name";
        Desc = desc;
        var data = _db.Load();
        Categories = data.Categories;

        IEnumerable<Product> prods = data.Products;
        if (SortBy == "quantity")
            prods = Desc ? prods.OrderByDescending(p => p.Quantity) : prods.OrderBy(p => p.Quantity);
        else if (SortBy == "min")
            prods = Desc ? prods.OrderByDescending(p => p.MinQuantity) : prods.OrderBy(p => p.MinQuantity);
        else
            prods = Desc ? prods.OrderByDescending(p => p.Name) : prods.OrderBy(p => p.Name);

        Products = prods.ToList();
    }
}
