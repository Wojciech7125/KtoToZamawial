using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Account;

public class LoginModel : PageModel
{
    private readonly Db _db;
    [BindProperty] public string Username { get; set; }
    [BindProperty] public string Password { get; set; }
    public string Error { get; set; }

    public LoginModel(Db db) => _db = db;

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        // wczytaj uzytkownikow z pliku JSON
        var data = _db.LoadUsers();

        // sprawdz czy login i haslo sie zgadzaja
        foreach (var u in data.Users)
        {
            if (u.Username == Username && u.Password == Password)
            {
                // tworzymy cookie z danymi uzytkownika
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.NameIdentifier, u.Id),
                    new Claim(ClaimTypes.Role, u.Role)
                };
                var identity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(identity));
                return RedirectToPage("/Index");
            }
        }

        // bledne dane logowania
        Error = "Nieprawidłowy login lub hasło.";
        return Page();
    }
}
