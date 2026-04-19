# 📋 Checklista — Adam

**Twój zakres:** moduł zarządzania produktami (lista, dodawanie, edycja, usuwanie)
**Szacowany czas:** ~9h
**Ważne:** zaczynasz w tygodniu 2, jak Gabryś skończy setup. Krzyś będzie na Ciebie czekał — skończ pierwszą wersję produktów szybko.

---

## 📚 Tydzień 1: przygotowanie (~2h)

### Zanim Gabryś skończy
- [ ] Przeczytaj [Razor Pages tutorial na Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start)
- [ ] Obczaj jak działa `@page`, `PageModel`, `OnGet()`, `OnPost()`, `[BindProperty]`
- [ ] Zobacz jak w szablonie Razor Pages wygląda formularz (przykład w `Pages/Privacy.cshtml`)

### Po tym jak Gabryś skończy
- [ ] Sklonuj repo: `git clone https://github.com/<gabrys>/Magazyn.git`
- [ ] Skopiuj `appsettings.Development.example.json` jako `appsettings.Development.json`
- [ ] Wklej connection string od Gabrysia (dostaniesz go prywatnie, NIE commituj!)
- [ ] Odpal projekt: `dotnet run`
- [ ] Sprawdź czy logowanie działa
- [ ] **Utwórz swojego brancha:** `git checkout -b feature/adam-produkty`

---

## 📦 Etap 1: Lista produktów (Tydzień 2, dni 1-2) — ~3h

### 1.1 Model `Product.cs`
- [ ] Utwórz folder `Models/` jeśli nie istnieje
- [ ] Utwórz plik `Models/Product.cs`:

```csharp
using System.ComponentModel.DataAnnotations;

namespace Magazyn.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Nazwa jest wymagana")]
    [StringLength(200)]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "SKU jest wymagane")]
    [StringLength(50)]
    public string Sku { get; set; }
    
    public int? CategoryId { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "Ilość musi być ≥ 0")]
    public int Quantity { get; set; }
    
    [Range(0, int.MaxValue)]
    public int MinQuantity { get; set; }
    
    [Range(0, 999999)]
    public decimal Price { get; set; }
}
```

- [ ] Zbuduj projekt: `dotnet build` — bez błędów?

### 1.2 Strona listy — `Pages/Products/Index.cshtml`
- [ ] Utwórz folder `Pages/Products/`
- [ ] W Visual Studio / VSCode: prawy na folder → Add → New Item → Razor Page (empty) → nazwa: `Index`
- [ ] VS utworzy dwa pliki: `Index.cshtml` i `Index.cshtml.cs`

### 1.3 Logika w `Index.cshtml.cs`
```csharp
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using MySqlConnector;
using System.Data;

[Authorize]
public class IndexModel : PageModel
{
    private readonly Db _db;
    public DataTable Produkty { get; set; }
    public string Szukaj { get; set; }
    
    public IndexModel(Db db) { _db = db; }
    
    public void OnGet(string szukaj)
    {
        Szukaj = szukaj ?? "";
        
        Produkty = _db.Query(@"
            SELECT p.id, p.name, p.sku, p.quantity, p.min_quantity, p.price,
                   c.name AS category_name
            FROM products p
            LEFT JOIN categories c ON p.category_id = c.id
            WHERE (@q = '' OR p.name LIKE @like OR p.sku LIKE @like)
            ORDER BY p.name",
            new MySqlParameter("@q", Szukaj),
            new MySqlParameter("@like", "%" + Szukaj + "%"));
    }
}
```

### 1.4 Widok `Index.cshtml`
- [ ] Otwórz `Index.cshtml`
- [ ] Wstaw kod z sekcji "Lista produktów" w README (plan_projektu_magazyn.html)
- [ ] Tabela z kolumnami: Nazwa, SKU, Kategoria, Ilość, Cena, akcje (Edytuj/Usuń)
- [ ] Formularz wyszukiwania na górze
- [ ] Klasa Bootstrap `table-danger` dla wierszy gdzie `quantity < min_quantity`
- [ ] Przycisk "+ Dodaj" linkujący do strony Create

### 1.5 Dodaj link w navbarze Wojtka
- [ ] Napisz do Wojtka: "Dodaj link 'Produkty' w layoucie, strona Products/Index"
- [ ] Lub sam dopisz: `<a class="nav-link" asp-page="/Products/Index">Produkty</a>`

### 1.6 Test
- [ ] `dotnet run`, zaloguj się, wejdź na `/Products`
- [ ] Pusta lista? Dobrze (jeszcze nie dodajemy produktów)
- [ ] Commit: `git add . && git commit -m "Adam: lista produktów"`
- [ ] Push: `git push origin feature/adam-produkty`

---

## ➕ Etap 2: Dodawanie produktu (Tydzień 2, dni 3-5) — ~3h

### 2.1 Strona `Create.cshtml`
- [ ] Add → New Item → Razor Page (empty) → nazwa: `Create`

### 2.2 Logika w `Create.cshtml.cs`
```csharp
[Authorize]
public class CreateModel : PageModel
{
    private readonly Db _db;
    public CreateModel(Db db) { _db = db; }
    
    [BindProperty] public Product Produkt { get; set; } = new();
    public DataTable Kategorie { get; set; }
    
    public void OnGet()
    {
        Kategorie = _db.Query("SELECT id, name FROM categories ORDER BY name");
    }
    
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            OnGet();
            return Page();
        }
        
        // Sprawdź unikalność SKU
        var exists = Convert.ToInt32(_db.Scalar(
            "SELECT COUNT(*) FROM products WHERE sku = @s",
            new MySqlParameter("@s", Produkt.Sku)));
        
        if (exists > 0)
        {
            ModelState.AddModelError("Produkt.Sku", "SKU już istnieje");
            OnGet();
            return Page();
        }
        
        _db.Exec(@"
            INSERT INTO products (name, sku, category_id, quantity, min_quantity, price)
            VALUES (@n, @s, @c, @q, @m, @p)",
            new MySqlParameter("@n", Produkt.Name),
            new MySqlParameter("@s", Produkt.Sku),
            new MySqlParameter("@c", (object)Produkt.CategoryId ?? DBNull.Value),
            new MySqlParameter("@q", Produkt.Quantity),
            new MySqlParameter("@m", Produkt.MinQuantity),
            new MySqlParameter("@p", Produkt.Price));
        
        TempData["Success"] = "Produkt dodany!";
        return RedirectToPage("Index");
    }
}
```

### 2.3 Widok `Create.cshtml`
- [ ] Formularz Bootstrapa z polami:
  - Nazwa (`asp-for="Produkt.Name"`)
  - SKU (`asp-for="Produkt.Sku"`)
  - Kategoria (dropdown z `Model.Kategorie`)
  - Ilość (`type="number"`)
  - Stan minimalny
  - Cena (`step="0.01"`)
- [ ] Dla każdego pola dodaj `<span asp-validation-for="...">` dla błędów
- [ ] Przyciski: "Zapisz" (submit) i "Anuluj" (link do Index)

### 2.4 Test
- [ ] Dodaj produkt testowy
- [ ] Sprawdź że się pokazuje na liście
- [ ] Spróbuj dodać drugi z tym samym SKU — powinna wyskoczyć walidacja
- [ ] Spróbuj bez nazwy — walidacja powinna zadziałać

### 2.5 Napisz do ekipy
- [ ] **Do Krzysia:** "Produkty działają, możesz już zacząć swoje operacje bo są produkty w bazie!"
- [ ] PR: wypchnij branch, otwórz PR na GitHubie, poproś kogoś o review

---

## ✏️ Etap 3: Edycja i usuwanie (Tydzień 3, dni 1-5) — ~3h

### 3.1 `Pages/Products/Edit.cshtml`
- [ ] Podobnie jak Create, ale:
  - `OnGet(int id)` ładuje istniejący produkt z bazy
  - `OnPost()` robi `UPDATE` zamiast `INSERT`
  - Walidacja unikalności SKU musi pomijać bieżący produkt: `AND id != @id`
- [ ] Kod bardzo podobny do Create — możesz skopiować i zmodyfikować

```csharp
public void OnGet(int id)
{
    var row = _db.Query("SELECT * FROM products WHERE id = @id",
        new MySqlParameter("@id", id)).Rows[0];
    
    Produkt = new Product
    {
        Id = (int)row["id"],
        Name = row["name"].ToString(),
        Sku = row["sku"].ToString(),
        CategoryId = row["category_id"] == DBNull.Value ? null : (int?)row["category_id"],
        Quantity = (int)row["quantity"],
        MinQuantity = (int)row["min_quantity"],
        Price = (decimal)row["price"]
    };
    
    Kategorie = _db.Query("SELECT id, name FROM categories ORDER BY name");
}
```

### 3.2 `Pages/Products/Delete.cshtml`
- [ ] Strona potwierdzenia: "Czy na pewno usunąć produkt X?"
- [ ] Przycisk "Tak, usuń" robi `OnPost()` → `DELETE FROM products WHERE id = @id`
- [ ] Przycisk "Anuluj" wraca do listy

### 3.3 Flash messages (komunikaty sukcesu)
- [ ] W `OnPost` każdej akcji: `TempData["Success"] = "Produkt zaktualizowany";`
- [ ] W layoucie Wojtka (w `_Layout.cshtml`) powinien być już kod który wyświetla `TempData["Success"]` w zielonym alercie — sprawdź z Wojtkiem

### 3.4 Test pełnego workflow
- [ ] Dodaj 3 produkty
- [ ] Edytuj jednemu cenę — sprawdź że się zmieniła
- [ ] Usuń jeden — sprawdź że zniknął z listy
- [ ] Spróbuj zmienić SKU na taki który już istnieje — powinna być walidacja

### 3.5 Finalne zrzuty i commit
- [ ] Dla Wojtka: zrzut ekranu listy produktów (do sekcji "Widoki aplikacji" w README)
- [ ] Commit wszystkich zmian
- [ ] PR do maina, code review, merge

---

## ✅ Koniec — twoja część gotowa

Sprawdzenie czy wszystko działa:

- [ ] Lista produktów wyświetla się poprawnie
- [ ] Wyszukiwanie po nazwie działa
- [ ] Wyszukiwanie po SKU działa
- [ ] Produkty poniżej minimum są na czerwono
- [ ] Dodawanie produktu działa
- [ ] Walidacja (puste pola, ujemna cena) działa
- [ ] Walidacja unikalnego SKU działa
- [ ] Edycja działa
- [ ] Usuwanie działa z potwierdzeniem
- [ ] Flash messages po sukcesie się pokazują

---

## 🗓️ Tydzień 4: testy i prezentacja

- [ ] Testuj całą aplikację z ekipą
- [ ] Pomóż innym jak znajdą bugi w Twoich produktach
- [ ] Przygotuj się żeby pokazać wykładowcy dodawanie/edycję/usuwanie produktu

---

## ⚠️ Typowe problemy

**"Cannot find table categories":**
→ Gabryś nie odpalił `database.sql` na bazie. Napisz do niego.

**Błąd "Duplicate entry ... for key 'sku'":**
→ Próbujesz dodać produkt z SKU które już istnieje. Walidacja powinna to złapać przed — sprawdź czy dobrze napisałeś sprawdzanie.

**Dropdown kategorii jest pusty:**
→ Tabela `categories` jest pusta. Gabryś powinien był dodać seed. Napisz do niego.

**Formularz nie waliduje pól:**
→ Sprawdź czy atrybuty `[Required]`, `[Range]` są na modelu `Product.cs`
→ Sprawdź czy `<span asp-validation-for="...">` jest w widoku
→ Sprawdź czy `!ModelState.IsValid` jest w `OnPost`

**"Kolumna 'category_name' nie istnieje":**
→ W SQL JOIN musi być alias: `c.name AS category_name`
