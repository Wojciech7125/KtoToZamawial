using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Categories; // kategorie

[Authorize(Roles = "kierownik")]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public List<Category> Categories { get; set; } = new();
    public List<Product> Products { get; set; } = new();
    public string Error { get; set; }

    [BindProperty] public string NewName { get; set; }
    [BindProperty] public int EditId { get; set; }
    [BindProperty] public string EditName { get; set; }
    [BindProperty] public int DeleteId { get; set; }

    public IndexModel(Db db) => _db = db;

    public void OnGet()
    {
        var data = _db.Load();
        Categories = data.Categories;
        Products = data.Products;
    }

    public IActionResult OnPostAdd()
    {
        if (string.IsNullOrWhiteSpace(NewName))
        {
            Error = "Nazwa jest wymagana.";
            var d = _db.Load(); Categories = d.Categories; Products = d.Products;
            return Page();
        }

        var data = _db.Load();

        // sprawdz czy nazwa juz istnieje
        if (data.Categories.Any(c => c.Name.Equals(NewName.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            Error = "Kategoria o takiej nazwie już istnieje.";
            Categories = data.Categories; Products = data.Products;
            return Page();
        }

        // nowe ID = max istniejacych + 1
        var cat = new Category
        {
            Id = data.Categories.Count > 0 ? data.Categories.Max(c => c.Id) + 1 : 1,
            Name = NewName.Trim()
        };
        data.Categories.Add(cat);
        _db.Save(data);
        TempData["Success"] = "Dodano kategorię.";
        return RedirectToPage();
    }

    public IActionResult OnPostEdit()
    {
        if (string.IsNullOrWhiteSpace(EditName))
        {
            Error = "Nazwa jest wymagana.";
            var d = _db.Load(); Categories = d.Categories; Products = d.Products;
            return Page();
        }

        var data = _db.Load();

        // sprawdz duplikat (pomijamy edytowana kategorie)
        if (data.Categories.Any(c => c.Id != EditId && c.Name.Equals(EditName.Trim(), StringComparison.OrdinalIgnoreCase)))
        {
            Error = "Kategoria o takiej nazwie już istnieje.";
            Categories = data.Categories; Products = data.Products;
            return Page();
        }

        // znajdz i podmien nazwe
        var cat = data.Categories.FirstOrDefault(c => c.Id == EditId);
        if (cat == null) return NotFound();
        cat.Name = EditName.Trim();
        _db.Save(data);
        TempData["Success"] = "Zapisano zmiany.";
        return RedirectToPage();
    }

    public IActionResult OnPostDelete()
    {
        var data = _db.Load();

        // blokuj usuniecie gdy sa powiazane produkty
        if (data.Products.Any(p => p.CategoryId == DeleteId))
        {
            Error = "Nie można usunąć kategorii z przypisanymi produktami.";
            Categories = data.Categories; Products = data.Products;
            return Page();
        }

        var cat = data.Categories.FirstOrDefault(c => c.Id == DeleteId);
        if (cat == null) return NotFound();
        data.Categories.Remove(cat);
        _db.Save(data);
        TempData["Success"] = "Usunięto kategorię.";
        return RedirectToPage();
    }
}
