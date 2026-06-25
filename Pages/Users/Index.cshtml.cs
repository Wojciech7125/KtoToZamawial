using Magazyn.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Users; // uzytkownicy

[Authorize(Roles = "kierownik")]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public List<User> Users { get; set; } = new();

    public IndexModel(Db db) => _db = db;

    public void OnGet()
    {
        Users = _db.LoadUsers().Users;
    }
}
