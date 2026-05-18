using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Products;

[Authorize]
public class DeleteModel : PageModel
{
    private readonly Db _db;
    public Product Product { get; set; }

    public DeleteModel(Db db) => _db = db;

    public IActionResult OnGet(int id)
    {
        Product = _db.Load().Products.FirstOrDefault(p => p.Id == id);
        if (Product == null) return NotFound();
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        var data = _db.Load();
        var product = data.Products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        data.Products.Remove(product);
        _db.Save(data);
        return RedirectToPage("Index");
    }
}
