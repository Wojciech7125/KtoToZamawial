using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Magazyn.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;

    public IndexModel(Db db) => _db = db;

    public void OnGet()
    {
    }
}
