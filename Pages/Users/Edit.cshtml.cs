using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Users; //ok

[Authorize(Roles = "kierownik")]
public class EditModel : PageModel
{
    private readonly Db _db;
    [BindProperty] public new User User { get; set; }

    public EditModel(Db db) => _db = db;

    public IActionResult OnGet(string id)
    {
        var data = _db.LoadUsers();

        // szukamy uzytkownika po ID
        User = data.Users.FirstOrDefault(u => u.Id == id);
        if (User == null) return NotFound();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var data = _db.LoadUsers();

        // znajdz indeks i podmien uzytkownika
        var idx = data.Users.FindIndex(u => u.Id == User.Id);
        if (idx == -1) return NotFound();
        data.Users[idx] = User;
        _db.SaveUsers(data);
        return RedirectToPage("Index");
    }
}
