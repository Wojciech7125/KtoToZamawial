# 📋 Checklista — Krzyś

**Twój zakres:** stany magazynowe, operacje IN/OUT, historia operacji
**Szacowany czas:** ~9h
**Ważne:** zaczynasz w tygodniu 2, ale **czekasz na Adama** — operacje magazynowe nie mają sensu bez produktów w bazie. Zacznij od rzeczy niezależnych (model, widok stanów) i poczekaj na produkty Adama do formularza operacji.

---

## 📚 Tydzień 1: przygotowanie (~2h)

### Zanim Gabryś skończy
- [ ] Przeczytaj [Razor Pages tutorial na Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start)
- [ ] Obczaj jak działa `@page`, `PageModel`, `OnGet()`, `OnPost()`, `[BindProperty]`
- [ ] Zrozum różnicę między `[BindProperty]` a `[BindProperty(SupportsGet = true)]` — dla filtrów w URL

### Po tym jak Gabryś skończy
- [ ] Sklonuj repo: `git clone https://github.com/<gabrys>/Magazyn.git`
- [ ] Skopiuj `appsettings.Development.example.json` jako `appsettings.Development.json`
- [ ] Wklej connection string (Gabryś Ci podeśle prywatnie)
- [ ] Odpal: `dotnet run` — sprawdź czy logowanie działa
- [ ] **Utwórz swojego brancha:** `git checkout -b feature/krzys-magazyn`

---

## 📊 Etap 1: Model i widok stanów (Tydzień 2, dni 1-3) — ~3h

To możesz robić równolegle z Adamem — nie potrzebujesz jeszcze jego produktów.

### 1.1 Model `Operation.cs`
- [ ] Utwórz plik `Models/Operation.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace Magazyn.Models;

public class Operation
{
    public int Id { get; set; }
    
    [Required]
    public int ProductId { get; set; }
    
    public string UserId { get; set; }
    
    [Required]
    [RegularExpression("IN|OUT")]
    public string Type { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być > 0")]
    public int Quantity { get; set; }
    
    public DateTime CreatedAt { get; set; }
}
```

- [ ] Zbuduj: `dotnet build` — bez błędów?

### 1.2 Strona stanów — `Pages/Stock/Index.cshtml`
- [ ] Utwórz folder `Pages/Stock/`
- [ ] Add → New Item → Razor Page (empty) → nazwa: `Index`

### 1.3 Logika w `Index.cshtml.cs`
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public DataTable Stany { get; set; }
    
    public IndexModel(Db db) { _db = db; }
    
    public void OnGet()
    {
        Stany = _db.Query(@"
            SELECT p.id, p.name, p.quantity, p.min_quantity,
                   CASE
                     WHEN p.quantity < p.min_quantity THEN 'NISKI'
                     WHEN p.quantity < p.min_quantity * 1.2 THEN 'BLISKO'
                     ELSE 'OK'
                   END AS status
            FROM products p
            ORDER BY (p.quantity < p.min_quantity) DESC, p.name");
    }
}
```

### 1.4 Widok `Index.cshtml`
- [ ] Tabela Bootstrapa z kolumnami: Produkt, Ilość, Minimum, Status
- [ ] Nad tabelą przyciski: "Przyjęcie (+)" i "Wydanie (-)" (na razie mogą linkować do Operation.cshtml który zrobisz dalej)
- [ ] Kolorowanie wierszy:

```html
@foreach (DataRow r in Model.Stany.Rows)
{
    var klasa = r["status"].ToString() switch
    {
        "NISKI" => "table-danger",
        "BLISKO" => "table-warning",
        _ => ""
    };
    <tr class="@klasa">
        <td>@r["name"]</td>
        <td>@r["quantity"]</td>
        <td>@r["min_quantity"]</td>
        <td>@r["status"]</td>
    </tr>
}
```

### 1.5 Dodaj link w navbarze
- [ ] Napisz do Wojtka albo dopisz sam: `<a class="nav-link" asp-page="/Stock/Index">Stany</a>`

### 1.6 Test
- [ ] `dotnet run`, wejdź na `/Stock`
- [ ] Jeśli są produkty Adama — zobaczysz je. Jeśli nie — pusta lista
- [ ] Commit: `git commit -m "Krzyś: model Operation + widok stanów"`

---

## ⬆️⬇️ Etap 2: Operacje IN/OUT (Tydzień 2, dni 4-7) — ~2h

**Warunek:** Adam skończył CRUD produktów (możesz wtedy testować dodawanie operacji). Jak jeszcze nie skończył — poczekaj albo testuj z produktami dodanymi ręcznie w Workbench.

### 2.1 Strona `Pages/Stock/Operation.cshtml`
- [ ] Add → New Item → Razor Page (empty) → nazwa: `Operation`

### 2.2 Logika w `Operation.cshtml.cs`
```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlConnector;
using System.Data;
using System.Security.Claims;

[Authorize]
public class OperationModel : PageModel
{
    private readonly Db _db;
    public OperationModel(Db db) { _db = db; }
    
    [BindProperty] public int ProductId { get; set; }
    [BindProperty] public int Quantity { get; set; }
    [BindProperty(SupportsGet = true)] public string Type { get; set; } = "IN";
    
    public DataTable Produkty { get; set; }
    public string Blad { get; set; }
    
    public void OnGet()
    {
        Produkty = _db.Query("SELECT id, name, quantity FROM products ORDER BY name");
    }
    
    public IActionResult OnPost()
    {
        // Walidacja przy wydaniu
        if (Type == "OUT")
        {
            var obecna = Convert.ToInt32(_db.Scalar(
                "SELECT quantity FROM products WHERE id = @id",
                new MySqlParameter("@id", ProductId)));
            
            if (obecna < Quantity)
            {
                Blad = "Za mało w magazynie! Dostępne: " + obecna;
                OnGet();
                return Page();
            }
        }
        
        // Pobierz ID zalogowanego usera (Identity)
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        // 1. Zmień stan produktu
        string znak = Type == "IN" ? "+" : "-";
        _db.Exec(
            $"UPDATE products SET quantity = quantity {znak} @q WHERE id = @id",
            new MySqlParameter("@q", Quantity),
            new MySqlParameter("@id", ProductId));
        
        // 2. Zapisz do historii
        _db.Exec(@"
            INSERT INTO operations (product_id, user_id, type, quantity)
            VALUES (@p, @u, @t, @q)",
            new MySqlParameter("@p", ProductId),
            new MySqlParameter("@u", userId),
            new MySqlParameter("@t", Type),
            new MySqlParameter("@q", Quantity));
        
        TempData["Success"] = $"Operacja {Type} wykonana";
        return RedirectToPage("Index");
    }
}
```

### 2.3 Widok `Operation.cshtml`
- [ ] Formularz z polami:
  - Radio buttons "Przyjęcie" / "Wydanie" (wartości "IN" / "OUT") — lub prosty select
  - Dropdown produktu (pokazuj nazwę + obecny stan: "Śruba M8 (520 szt.)")
  - Input ilości (number, min 1)
- [ ] Pokazuj błąd z `Model.Blad` jeśli nie null (Bootstrap alert-danger)
- [ ] Przyciski "Zatwierdź" i "Anuluj"

### 2.4 Test
- [ ] Dodaj kilka produktów (Adam już zrobił jego formularz? Super, użyj go)
- [ ] Zrób przyjęcie +100 sztuk produktu
- [ ] Sprawdź że stan wzrósł
- [ ] Zrób wydanie 30 sztuk
- [ ] Sprawdź że stan spadł do 70
- [ ] Spróbuj wydać 1000 sztuk — powinno wyskoczyć "Za mało w magazynie"
- [ ] Sprawdź w bazie że wpisy w `operations` się dodały

### 2.5 Commit i push
- [ ] `git commit -m "Krzyś: operacje IN/OUT"`
- [ ] `git push origin feature/krzys-magazyn`

---

## 📜 Etap 3: Historia operacji (Tydzień 3, dni 1-4) — ~3h

### 3.1 Strona `Pages/Stock/History.cshtml`
- [ ] Add → New Item → Razor Page (empty) → nazwa: `History`

### 3.2 Logika w `History.cshtml.cs`
```csharp
[Authorize]
public class HistoryModel : PageModel
{
    private readonly Db _db;
    public HistoryModel(Db db) { _db = db; }
    
    [BindProperty(SupportsGet = true)] public DateTime? Od { get; set; }
    [BindProperty(SupportsGet = true)] public DateTime? Do { get; set; }
    
    public DataTable Operacje { get; set; }
    
    public void OnGet()
    {
        var od = Od ?? DateTime.Today.AddMonths(-1);
        var do_ = Do ?? DateTime.Today.AddDays(1);
        
        Operacje = _db.Query(@"
            SELECT o.created_at, o.type, o.quantity,
                   p.name AS product_name,
                   u.UserName AS user_name
            FROM operations o
            JOIN products p ON p.id = o.product_id
            LEFT JOIN AspNetUsers u ON u.Id = o.user_id
            WHERE o.created_at BETWEEN @od AND @do
            ORDER BY o.created_at DESC",
            new MySqlParameter("@od", od),
            new MySqlParameter("@do", do_));
    }
}
```

### 3.3 Widok `History.cshtml`
- [ ] Formularz filtrów (method=get) z dwoma `<input type="date">` dla Od/Do
- [ ] Przycisk "Filtruj"
- [ ] Tabela z kolumnami: Data, Typ, Produkt, Ilość, Użytkownik
- [ ] Kolorowanie typu:
  - IN → zielony tekst
  - OUT → czerwony tekst

### 3.4 Dodaj link w navbarze
- [ ] Napisz do Wojtka: `<a class="nav-link" asp-page="/Stock/History">Historia</a>`

### 3.5 Test
- [ ] Zrób kilka operacji (przyjęcia i wydania)
- [ ] Wejdź na `/Stock/History` — powinieneś zobaczyć listę
- [ ] Przefiltruj po datach — sprawdź że filtr działa
- [ ] Sprawdź że widać kto zrobił operację (login)

---

## 🧪 Etap 4: Testy (Tydzień 3, dni 5-7) — ~1h

### 4.1 Scenariusz pełnego testu
- [ ] Dodaj produkt z ilością 100 i min=50 (OK)
- [ ] Zrób wydanie 60 szt. → stan spadnie do 40, status powinien być NISKI (czerwony)
- [ ] Zrób przyjęcie 80 szt. → stan 120, status OK
- [ ] Sprawdź historię — dwie operacje z Twoim loginem

### 4.2 Edge cases
- [ ] Co jak wydaję 0 szt.? Walidacja powinna to odrzucić (`[Range(1, ...)]`)
- [ ] Co jak wydaję więcej niż mam? Walidacja powinna to złapać
- [ ] Co jak nie wybrano produktu? Walidacja

### 4.3 Finalizacja
- [ ] Commit wszystkich zmian
- [ ] Push
- [ ] PR na GitHubie → code review → merge

---

## ✅ Koniec — twoja część gotowa

Sprawdzenie:

- [ ] Widok stanów magazynowych pokazuje wszystkie produkty
- [ ] Produkty poniżej minimum są wyróżnione na czerwono
- [ ] Produkty blisko minimum (20%) na żółto
- [ ] Przyjęcie IN zwiększa stan
- [ ] Wydanie OUT zmniejsza stan
- [ ] Nie można wydać więcej niż jest w magazynie
- [ ] Każda operacja zapisuje się do historii z User ID
- [ ] Historia pokazuje listę operacji z możliwością filtru po dacie
- [ ] Historia pokazuje kto zrobił operację (login)

---

## 🗓️ Tydzień 4: testy i prezentacja

- [ ] Testuj całą aplikację z ekipą
- [ ] Na prezentacji pokaż przepływ: dodaj produkt → przyjmij towar → wydaj towar → sprawdź historię

---

## ⚠️ Typowe problemy

**"Kolumna `quantity` jest niewystarczająca":**
→ Wydanie większe niż stan. Powinieneś walidować przed. Sprawdź czy Twój kod to robi.

**"Foreign key constraint fails":**
→ Usuwasz produkt który ma operacje w historii. Baza Ci na to nie pozwoli, bo są FK. 
→ Rozwiązanie: Adam powinien sprawdzić czy produkt ma operacje przed usuwaniem, albo użyć `ON DELETE CASCADE` w schemacie (do uzgodnienia z Gabrysiem).

**User w historii ma `null`:**
→ `User.FindFirstValue(ClaimTypes.NameIdentifier)` zwraca null. Sprawdź czy strona Operation ma `[Authorize]` i czy jesteś zalogowany.
→ Upewnij się że używasz namespace `System.Security.Claims`.

**LEFT JOIN na AspNetUsers nie zwraca loginu:**
→ W SQL kolumna nazywa się `UserName` (camelCase w MySQL to może być wrażliwa sprawa). Spróbuj: `\`UserName\`` w backticks.

**Po kliknięciu Zatwierdź strona się odświeża ale nic się nie zmienia:**
→ Sprawdź czy formularz ma `method="post"`
→ Sprawdź czy metoda się nazywa `OnPost()` a nie `OnGet()`
→ Sprawdź czy nie ma błędów walidacji (dodaj `<div asp-validation-summary="All">`)
