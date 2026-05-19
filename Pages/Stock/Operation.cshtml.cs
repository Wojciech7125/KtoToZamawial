using System.Security.Claims;
using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Stock;

[Authorize]
public class OperationModel : PageModel
{
    private readonly Db _db;
    public List<Product> Products { get; set; } = new();
    [BindProperty] public int ProductId { get; set; }
    [BindProperty] public string Type { get; set; }
    [BindProperty] public int Quantity { get; set; }
    public string Error { get; set; }

    public OperationModel(Db db) => _db = db;

    public void OnGet()
    {
        Products = _db.Load().Products;
    }

    public IActionResult OnPost()
    {
        var data = _db.Load();

        var product = data.Products.FirstOrDefault(p => p.Id == ProductId);
        if (product == null)
        {
            Error = "Nie znaleziono produktu.";
            Products = data.Products;
            return Page();
        }

        // przy wydaniu sprawdz czy jest wystarczajaco duzo towaru
        if (Type == "OUT" && product.Quantity < Quantity)
        {
            Error = "Za mało towaru na stanie.";
            Products = data.Products;
            return Page();
        }

        if (Type == "IN") product.Quantity += Quantity;
        else product.Quantity -= Quantity;

        var op = new Operation
        {
            Id = data.Operations.Count > 0 ? data.Operations.Max(o => o.Id) + 1 : 1,
            ProductId = ProductId,
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "?",
            Type = Type,
            Quantity = Quantity,
            CreatedAt = DateTime.Now
        };
        data.Operations.Add(op);
        _db.Save(data);
        return RedirectToPage("Index");
    }
}

