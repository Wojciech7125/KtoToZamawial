using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Products;

[Authorize]
public class CreateModel : PageModel
{
    private readonly Db _db;
    [BindProperty] public Product Product { get; set; } = new();
    public List<Category> Categories { get; set; } = new();

    public CreateModel(Db db) => _db = db;

    public void OnGet()
    {
        // ladujemy kategorie do dropdowna
        Categories = _db.Load().Categories;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            Categories = _db.Load().Categories;
            return Page();
        }

        var data = _db.Load();

        // nowe ID = max istniejacych + 1
        Product.Id = data.Products.Count > 0 ? data.Products.Max(p => p.Id) + 1 : 1;
        data.Products.Add(Product);
        _db.Save(data);
        return RedirectToPage("Index");
    }
}
