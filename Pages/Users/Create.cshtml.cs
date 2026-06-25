using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Users; //

[Authorize(Roles = "kierownik")]
public class CreateModel : PageModel
{
    private readonly Db _db;
    [BindProperty] public new User User { get; set; } = new() { Role = "pracownik" };

    public CreateModel(Db db) => _db = db;

    public void OnGet() { }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid) return Page();

        var data = _db.LoadUsers();

        // nowe ID = max istniejacych (jako int) + 1
        User.Id = data.Users.Count > 0
            ? (data.Users.Max(u => int.Parse(u.Id)) + 1).ToString()
            : "1";

        data.Users.Add(User);
        _db.SaveUsers(data);
        return RedirectToPage("Index");
    }
}
