using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public int TotalProducts { get; set; }
    public int LowStock { get; set; }
    public int TotalOperations { get; set; }
    public List<Product> Products { get; set; } = new();

    public IndexModel(Db db) => _db = db;

    public void OnGet()
    {
        var data = _db.Load();
        Products = data.Products;

        // podstawowe statystyki na strone glowna
        TotalProducts = data.Products.Count;
        LowStock = data.Products.Count(p => p.Quantity < p.MinQuantity);
        TotalOperations = data.Operations.Count;
    }
}
