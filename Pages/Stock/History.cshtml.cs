using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Stock;

[Authorize]
public class HistoryModel : PageModel
{
    private readonly Db _db;
    public List<Operation> Operations { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }

    public HistoryModel(Db db) => _db = db;

    public void OnGet(DateTime? from, DateTime? to)
    {
        From = from;
        To = to;
        var data = _db.Load();
        Products = data.Products;

        Operations = data.Operations
            .Where(o => (from == null || o.CreatedAt >= from) && (to == null || o.CreatedAt <= to))
            .OrderByDescending(o => o.CreatedAt)
            .ToList();
    }
}
