using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages.Account;

public class LoginModel : PageModel
{
    private readonly IConfiguration _cfg;
    [BindProperty] public string Username { get; set; }
    [BindProperty] public string Password { get; set; }
    public string Error { get; set; }

    public LoginModel(IConfiguration cfg) => _cfg = cfg;

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        // wczytaj uzytkownikow z pliku JSON
        var path = _cfg["UsersFile"] ?? "data/users.json";
        var json = System.IO.File.ReadAllText(path);
        var doc = JsonDocument.Parse(json);
        var users = doc.RootElement.GetProperty("users");

        // sprawdz czy login i haslo sie zgadzaja
        foreach (var u in users.EnumerateArray())
        {
            if (u.GetProperty("username").GetString() == Username &&
                u.GetProperty("password").GetString() == Password)
            {
                // tworzymy cookie z danymi uzytkownika
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username),
                    new Claim(ClaimTypes.NameIdentifier, u.GetProperty("id").GetString())
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
