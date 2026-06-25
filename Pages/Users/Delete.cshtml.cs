using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Users;

[Authorize(Roles = "kierownik")]
public class DeleteModel : PageModel
{
    private readonly Db _db;
    public new User User { get; set; }

    public DeleteModel(Db db) => _db = db;

    public IActionResult OnGet(string id)
    {
        User = _db.LoadUsers().Users.FirstOrDefault(u => u.Id == id);
        if (User == null) return NotFound();
        return Page();
    }

    public IActionResult OnPost(string id)
    {
        var data = _db.LoadUsers();
        var user = data.Users.FirstOrDefault(u => u.Id == id);
        if (user == null) return NotFound();

        // usuwamy uzytkownika z listy
        data.Users.Remove(user);
        _db.SaveUsers(data);
        return RedirectToPage("Index");
    }
}
