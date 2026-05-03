# 📋 Checklista — Wojtek

**Twój zakres:** layout, dashboard, dostosowanie Identity UI, raport PDF
**Szacowany czas:** ~10h
**Ważne:** robisz layout w tygodniu 2 (żeby reszta miała gdzie linkować), dashboard i PDF w tygodniu 3 (potrzebuje danych z bazy Adama i Krzysia).

---

## 📚 Tydzień 1: przygotowanie (~1h)

### Zanim Gabryś skończy
- [ ] Przeczytaj [Razor Pages tutorial na Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start)
- [ ] Zobacz jak działa `_Layout.cshtml` i `@RenderBody()`
- [ ] Obczaj [Bootstrap 5 docs](https://getbootstrap.com/docs/5.3/) — navbar i cards
- [ ] Zerknij na [QuestPDF documentation](https://www.questpdf.com/api-reference/document.html) — strony, tabele, nagłówki

### Po tym jak Gabryś skończy
- [ ] Sklonuj repo: `git clone https://github.com/<gabrys>/Magazyn.git`
- [ ] Skopiuj `appsettings.Development.example.json` jako `appsettings.Development.json`
- [ ] Wklej connection string (Gabryś Ci podeśle)
- [ ] Odpal: `dotnet run` i sprawdź rejestrację/logowanie
- [ ] **Utwórz swojego brancha:** `git checkout -b feature/wojtek-ui`

---

## 🎨 Etap 1: Layout aplikacji (Tydzień 2, dni 1-3) — ~2h

### 1.1 Edytuj `Pages/Shared/_Layout.cshtml`
Plik już istnieje (domyślny z szablonu). Trzeba go przerobić.

- [ ] Otwórz `Pages/Shared/_Layout.cshtml`
- [ ] Zmień tytuł strony i brand w navbarze na "Magazyn"
- [ ] Dodaj linki do wszystkich modułów (Produkty, Stany, Historia, Raporty)

Przykład navbara:
```html
<nav class="navbar navbar-expand-sm navbar-dark bg-dark border-bottom box-shadow mb-3">
    <div class="container-fluid">
        <a class="navbar-brand" asp-page="/Index">Magazyn</a>
        <ul class="navbar-nav me-auto">
            <li class="nav-item">
                <a class="nav-link" asp-page="/Products/Index">Produkty</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" asp-page="/Stock/Index">Stany</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" asp-page="/Stock/History">Historia</a>
            </li>
            <li class="nav-item">
                <a class="nav-link" asp-page="/Reports/Index">Raporty</a>
            </li>
        </ul>
        <partial name="_LoginPartial" />
    </div>
</nav>
```

### 1.2 Flash messages — obsługa TempData["Success"]
Adam i Krzyś po każdej akcji zapisują `TempData["Success"] = "..."`. Trzeba to gdzieś pokazać.

- [ ] W `_Layout.cshtml` dodaj po navbarze, przed `@RenderBody()`:
```html
<div class="container">
    @if (TempData["Success"] != null)
    {
        <div class="alert alert-success alert-dismissible fade show" role="alert">
            @TempData["Success"]
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    }
    @if (TempData["Error"] != null)
    {
        <div class="alert alert-danger alert-dismissible fade show" role="alert">
            @TempData["Error"]
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    }
</div>
```

### 1.3 Sprawdź `_LoginPartial.cshtml`
Ten plik już istnieje w szablonie (`Pages/Shared/_LoginPartial.cshtml`). Pokazuje "Hello username" + Logout gdy zalogowany, lub Register/Login gdy nie.

- [ ] Otwórz i sprawdź że działa
- [ ] Spolszcz teksty ("Hello" → "Witaj", "Logout" → "Wyloguj", "Login" → "Zaloguj", "Register" → "Zarejestruj")

### 1.4 Test
- [ ] `dotnet run`, zaloguj się
- [ ] Navbar pokazuje wszystkie linki? (Na razie większość nie działa bo inni nie skończyli — to normalne, kliknij żeby sprawdzić że 404 jest na właściwych URL)
- [ ] Twój login widoczny w prawym górnym rogu?
- [ ] Commit: `git commit -m "Wojtek: layout aplikacji"`

---

## 🔐 Etap 2: Dostosowanie Identity UI (Tydzień 2, dni 4-5) — ~2h

Domyślne strony Identity są mało zgrabne — trzeba je trochę ogarnąć.

### 2.1 Wygeneruj pliki Identity (scaffold)
Te pliki są w bibliotece, nie widać ich bezpośrednio. Trzeba je "wygenerować" żeby je edytować:

- [ ] W Visual Studio: prawy na projekt → Add → New Scaffolded Item → Identity
- [ ] Zaznacz tylko: `Account/Login`, `Account/Register`, `Account/Logout`
- [ ] Wybierz swój `ApplicationDbContext`
- [ ] Kliknij Add

Pliki pojawią się w `Areas/Identity/Pages/Account/`.

**Alternatywa przez CLI:**
```bash
dotnet tool install --global dotnet-aspnet-codegenerator
dotnet aspnet-codegenerator identity -dc Magazyn.Data.ApplicationDbContext --files "Account.Login;Account.Register;Account.Logout"
```

### 2.2 Spolszcz strony logowania i rejestracji
- [ ] Otwórz `Areas/Identity/Pages/Account/Login.cshtml`
- [ ] Zmień teksty na polskie:
  - "Log in" → "Zaloguj się"
  - "Email" → "Email lub login"
  - "Password" → "Hasło"
  - "Remember me?" → "Zapamiętaj mnie"
  - Błędy walidacji ("The Email field is required") też warto spolszczyć

- [ ] To samo w `Register.cshtml`:
  - "Register" → "Rejestracja"
  - "Create a new account" → "Utwórz nowe konto"
  - "Password" → "Hasło"
  - "Confirm password" → "Powtórz hasło"

### 2.3 Dodaj spójny wygląd
- [ ] Zmień `<h1>Log in</h1>` na `<h1 class="h3 mb-3">Zaloguj się</h1>`
- [ ] Dodaj klasę `card` dookoła formularza żeby wyglądał jak w reszcie aplikacji
- [ ] Zobacz czy navbar pokazuje się nad tymi stronami (powinien, bo dziedziczą z `_Layout`)

### 2.4 Test
- [ ] Zarejestruj nowe konto — proces po polsku?
- [ ] Zaloguj się — po polsku?
- [ ] Wyloguj — po polsku?
- [ ] Commit: `git commit -m "Wojtek: dostosowanie Identity UI"`

---

## 📊 Etap 3: Dashboard (Tydzień 3, dni 1-4) — ~3h

**Warunek:** Adam i Krzyś skończyli swoje moduły (są produkty i operacje w bazie).

### 3.1 Strona główna — `Pages/Index.cshtml`
Plik już istnieje (Welcome page). Trzeba go przerobić w dashboard.

### 3.2 Logika w `Index.cshtml.cs`
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    
    public int LiczbaProduktow { get; set; }
    public int LiczbaKategorii { get; set; }
    public int LiczbaAlertow { get; set; }
    public int OperacjeDzis { get; set; }
    public DataTable Alerty { get; set; }
    
    public IndexModel(Db db) { _db = db; }
    
    public void OnGet()
    {
        LiczbaProduktow = Convert.ToInt32(_db.Scalar(
            "SELECT COUNT(*) FROM products"));
        
        LiczbaKategorii = Convert.ToInt32(_db.Scalar(
            "SELECT COUNT(*) FROM categories"));
        
        LiczbaAlertow = Convert.ToInt32(_db.Scalar(
            "SELECT COUNT(*) FROM products WHERE quantity < min_quantity"));
        
        OperacjeDzis = Convert.ToInt32(_db.Scalar(
            "SELECT COUNT(*) FROM operations WHERE DATE(created_at) = CURDATE()"));
        
        Alerty = _db.Query(
            "SELECT name, quantity, min_quantity FROM products WHERE quantity < min_quantity ORDER BY (quantity * 1.0 / min_quantity)");
    }
}
```

### 3.3 Widok `Index.cshtml`
```html
@page
@model IndexModel
@{
    ViewData["Title"] = "Strona główna";
}

<div class="container">
    <h1 class="mb-4">Witaj, @User.Identity.Name</h1>
    
    <!-- 4 kafelki -->
    <div class="row g-3 mb-4">
        <div class="col-md-3">
            <div class="card text-center">
                <div class="card-body">
                    <p class="text-muted mb-1">Produktów</p>
                    <h2 class="mb-0">@Model.LiczbaProduktow</h2>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-center">
                <div class="card-body">
                    <p class="text-muted mb-1">Kategorii</p>
                    <h2 class="mb-0">@Model.LiczbaKategorii</h2>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-center @(Model.LiczbaAlertow > 0 ? "bg-danger text-white" : "")">
                <div class="card-body">
                    <p class="mb-1">Alerty ⚠</p>
                    <h2 class="mb-0">@Model.LiczbaAlertow</h2>
                </div>
            </div>
        </div>
        <div class="col-md-3">
            <div class="card text-center">
                <div class="card-body">
                    <p class="text-muted mb-1">Operacje dziś</p>
                    <h2 class="mb-0">@Model.OperacjeDzis</h2>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Lista alertów jeśli są -->
    @if (Model.Alerty.Rows.Count > 0)
    {
        <div class="card border-danger">
            <div class="card-header bg-danger text-white">
                <strong>⚠ Produkty poniżej stanu minimalnego</strong>
            </div>
            <ul class="list-group list-group-flush">
                @foreach (System.Data.DataRow r in Model.Alerty.Rows)
                {
                    <li class="list-group-item d-flex justify-content-between">
                        <span>@r["name"]</span>
                        <span class="text-danger">
                            @r["quantity"] / @r["min_quantity"]
                        </span>
                    </li>
                }
            </ul>
        </div>
    }
    
    <!-- Szybkie linki -->
    <div class="row g-3 mt-4">
        <div class="col-md-3">
            <a asp-page="/Products/Index" class="btn btn-outline-primary w-100 p-3">
                📦 Produkty
            </a>
        </div>
        <div class="col-md-3">
            <a asp-page="/Stock/Index" class="btn btn-outline-primary w-100 p-3">
                📊 Stany
            </a>
        </div>
        <div class="col-md-3">
            <a asp-page="/Stock/History" class="btn btn-outline-primary w-100 p-3">
                📜 Historia
            </a>
        </div>
        <div class="col-md-3">
            <a asp-page="/Reports/Index" class="btn btn-outline-primary w-100 p-3">
                📄 Raporty
            </a>
        </div>
    </div>
</div>
```

### 3.4 Test
- [ ] Wejdź na stronę główną (`/`) po zalogowaniu
- [ ] Widzisz 4 kafelki z liczbami?
- [ ] Kafelek "Alerty" jest czerwony gdy są produkty poniżej minimum?
- [ ] Lista alertów wyświetla się pod kafelkami?
- [ ] Linki na dole działają?

---

## 📄 Etap 4: Raport PDF (Tydzień 3, dni 5-7) — ~2h

### 4.1 Strona `Pages/Reports/Index.cshtml`
- [ ] Utwórz folder `Pages/Reports/`
- [ ] Add → New Item → Razor Page (empty) → nazwa: `Index`

### 4.2 Logika w `Index.cshtml.cs`
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public IndexModel(Db db) { _db = db; }
    
    public void OnGet() { }
    
    // /Reports?handler=Stan
    public IActionResult OnGetStan()
    {
        var dane = _db.Query(
            "SELECT name, sku, quantity, min_quantity, price FROM products ORDER BY name");
        
        byte[] pdf = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                
                page.Header().Text($"Raport stanu magazynu — {DateTime.Now:dd.MM.yyyy}")
                    .FontSize(16).Bold();
                
                page.Content().Table(t =>
                {
                    t.ColumnsDefinition(c =>
                    {
                        c.RelativeColumn(3);  // nazwa
                        c.RelativeColumn();    // sku
                        c.RelativeColumn();    // ilość
                        c.RelativeColumn();    // min
                        c.RelativeColumn();    // cena
                    });
                    
                    t.Header(h =>
                    {
                        h.Cell().Text("Nazwa").Bold();
                        h.Cell().Text("SKU").Bold();
                        h.Cell().Text("Ilość").Bold();
                        h.Cell().Text("Min.").Bold();
                        h.Cell().Text("Cena").Bold();
                    });
                    
                    foreach (System.Data.DataRow r in dane.Rows)
                    {
                        t.Cell().Text(r["name"].ToString());
                        t.Cell().Text(r["sku"].ToString());
                        t.Cell().Text(r["quantity"].ToString());
                        t.Cell().Text(r["min_quantity"].ToString());
                        t.Cell().Text($"{r["price"]} zł");
                    }
                });
                
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("Strona ");
                    x.CurrentPageNumber();
                    x.Span(" z ");
                    x.TotalPages();
                });
            });
        }).GeneratePdf();
        
        return File(pdf, "application/pdf", $"stan-magazynu-{DateTime.Now:yyyy-MM-dd}.pdf");
    }
}
```

### 4.3 Widok `Index.cshtml`
```html
@page
@model IndexModel
@{
    ViewData["Title"] = "Raporty";
}

<h1>Raporty PDF</h1>

<p class="text-muted">Wybierz raport który chcesz pobrać.</p>

<div class="list-group">
    <a asp-page-handler="Stan" class="list-group-item list-group-item-action">
        <div class="d-flex w-100 justify-content-between">
            <h5 class="mb-1">📄 Stan magazynu</h5>
            <small>PDF</small>
        </div>
        <p class="mb-1">Lista wszystkich produktów z ilościami i cenami.</p>
    </a>
</div>
```

### 4.4 Test
- [ ] Wejdź na `/Reports`
- [ ] Kliknij "Stan magazynu"
- [ ] Powinien pobrać się plik PDF z listą produktów
- [ ] Otwórz PDF — ładnie wygląda?
- [ ] Commit: `git commit -m "Wojtek: dashboard + raport PDF"`

---

## 🎨 Etap 5: Ostatnie szlify (Tydzień 3-4) — ~1h

### 5.1 `wwwroot/css/site.css`
- [ ] Drobne poprawki stylu jeśli coś nie wygląda
- [ ] Np. padding w kafelkach dashboardu, kolor navbar, itp.

### 5.2 Favicon (opcjonalnie)
- [ ] Znajdź ikonkę magazynu (np. emoji 📦 albo pobierz z flaticon)
- [ ] Podmień `wwwroot/favicon.ico`

### 5.3 Stopka (opcjonalnie)
- [ ] W `_Layout.cshtml` w istniejącej `<footer>` zmień tekst na "Magazyn © 2026 Gabryś, Adam, Krzyś, Wojtek"

---

## ✅ Koniec — twoja część gotowa

Sprawdzenie:

- [ ] Layout działa na wszystkich stronach
- [ ] Navbar pokazuje linki do wszystkich modułów
- [ ] Flash messages (TempData["Success"]) pojawiają się po akcjach
- [ ] Login/Register/Logout po polsku
- [ ] Dashboard z 4 kafelkami
- [ ] Kafelek alertów czerwony gdy są alerty
- [ ] Lista produktów poniżej minimum pod kafelkami
- [ ] Raport PDF stanu magazynu pobiera się i otwiera

---

## 🗓️ Tydzień 4: testy i prezentacja

- [ ] Testuj całą aplikację z ekipą
- [ ] Zrzuty ekranu do README (wszystkie 9 widoków)
- [ ] Na prezentacji: zacznij od pokazania dashboardu po zalogowaniu — robi wrażenie!

---

## ⚠️ Typowe problemy

**"QuestPDF nie generuje PDF":**
→ Brakuje `QuestPDF.Settings.License = LicenseType.Community` w `Program.cs`. Sprawdź z Gabrysiem.

**Dashboard pokazuje zero dla wszystkiego:**
→ Baza jest pusta. Poczekaj aż Adam i Krzyś dodadzą kilka produktów i operacji.

**Flash messages się nie pokazują:**
→ `TempData` w `_Layout.cshtml` musi być na górze, przed `@RenderBody()`. Sprawdź kolejność.

**Identity UI nie widzi po scaffoldingu:**
→ Czasem trzeba zrestartować VS/VS Code po scaffoldzie. Restart + rebuild.

**PDF pobiera się pusty lub uszkodzony:**
→ Sprawdź że `return File(pdf, "application/pdf", "nazwa.pdf")` ma `byte[]` w pierwszym argumencie
→ Sprawdź w konsoli browser network: czy response ma `Content-Type: application/pdf`

**"The type or namespace name 'QuestPDF' could not be found":**
→ Paczka nie zainstalowana. `dotnet add package QuestPDF` w katalogu projektu.
