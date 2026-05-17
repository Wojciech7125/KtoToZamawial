using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Products;

[Authorize]
public class EditModel : PageModel
{
    private readonly Db _db;
    [BindProperty] public Product Product { get; set; }
    public List<Category> Categories { get; set; } = new();

    public EditModel(Db db) => _db = db;

    public IActionResult OnGet(int id)
    {
        var data = _db.Load();

        // szukamy produktu po ID
        Product = data.Products.FirstOrDefault(p => p.Id == id);
        if (Product == null) return NotFound();
        Categories = data.Categories;
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Categories = _db.Load().Categories;
            return Page();
        }

        var data = _db.Load();

        // znajdz indeks i podmien produkt
        var idx = data.Products.FindIndex(p => p.Id == Product.Id);
        if (idx == -1) return NotFound();
        data.Products[idx] = Product;
        _db.Save(data);
        return RedirectToPage("Index");
    }
}
