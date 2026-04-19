# 📖 Słownik nazw — Magazyn

**Do czego to służy:** żeby każdy używał tych samych nazw. Adam, Krzyś i Wojtek czytają z tabeli `products` — wszyscy muszą używać dokładnie tej samej nazwy kolumny, klasy, pola. Inaczej bajzel.

**Zasada:** bazy danych i kod po angielsku (standard). Teksty widoczne dla użytkownika (labelki, przyciski) po polsku.

---

## 🗄️ Tabele w bazie

### `users` → obsługiwane przez Identity jako `AspNetUsers`
Nie dotykamy ręcznie — Identity zarządza.

Kolumny potrzebne innym (do JOIN):
| Kolumna | Typ | Co to |
|---|---|---|
| `Id` | VARCHAR(450) | GUID użytkownika (string, nie int!) |
| `UserName` | VARCHAR | Login (który user wpisał przy rejestracji) |
| `Email` | VARCHAR | Email użytkownika |

### `categories`
| Kolumna | Typ | Co to |
|---|---|---|
| `id` | INT | Klucz główny, auto-increment |
| `name` | VARCHAR(100) | Nazwa kategorii, unikalna (np. "Elektronika") |

### `products`
| Kolumna | Typ | Co to |
|---|---|---|
| `id` | INT | Klucz główny, auto-increment |
| `name` | VARCHAR(200) | Nazwa produktu (np. "Śruba M8") |
| `sku` | VARCHAR(50) | Kod produktu, unikalny (np. "SR-M8") |
| `category_id` | INT (FK) | ID kategorii, może być NULL |
| `quantity` | INT | Aktualna ilość w magazynie |
| `min_quantity` | INT | Stan minimalny (poniżej = alert) |
| `price` | DECIMAL(10,2) | Cena jednostkowa |

### `operations`
| Kolumna | Typ | Co to |
|---|---|---|
| `id` | INT | Klucz główny, auto-increment |
| `product_id` | INT (FK) | ID produktu którego dotyczy operacja |
| `user_id` | VARCHAR(450) (FK) | ID użytkownika (string z Identity!) |
| `type` | ENUM('IN','OUT') | IN = przyjęcie, OUT = wydanie |
| `quantity` | INT | Ilość w tej operacji |
| `created_at` | DATETIME | Kiedy wykonano (domyślnie CURRENT_TIMESTAMP) |

**Konwencja bazy:**
- Nazwy tabel: **liczba mnoga, małe litery** (`products`, `categories`, `operations`)
- Nazwy kolumn: **snake_case** (`product_id`, `min_quantity`, `created_at`)
- Klucze obce: `<tabela>_id` (`category_id`, `product_id`, `user_id`)

---

## 🏛️ Klasy C# (modele)

**Konwencja klas:** PascalCase, liczba pojedyncza, po angielsku.

### `ApplicationUser` — Gabryś
Dziedziczy z `IdentityUser`. Na razie bez dodatkowych pól.

```csharp
public class ApplicationUser : IdentityUser
{
    // Pusta - wystarczy dziedziczenie
}
```

### `Category` — Adam
```csharp
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

### `Product` — Adam
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }        // nazwa produktu
    public string Sku { get; set; }         // kod SKU
    public int? CategoryId { get; set; }    // FK do kategorii (może być null)
    public int Quantity { get; set; }       // aktualny stan
    public int MinQuantity { get; set; }    // stan minimalny
    public decimal Price { get; set; }      // cena jednostkowa
}
```

### `Operation` — Krzyś
```csharp
public class Operation
{
    public int Id { get; set; }
    public int ProductId { get; set; }      // FK do produktu
    public string UserId { get; set; }      // FK do Identity (STRING!)
    public string Type { get; set; }        // "IN" lub "OUT"
    public int Quantity { get; set; }       // ilość
    public DateTime CreatedAt { get; set; } // data operacji
}
```

**Konwencja klas C#:**
- Nazwy klas: **PascalCase, pojedyncza liczba** (`Product`, nie `Products`)
- Properties: **PascalCase** (`Name`, `ProductId`, `CreatedAt`)
- Relacje FK: **bez `Id` na końcu nazwy klasy** (`CategoryId` not `CategoryID`)

---

## 🔄 Mapowanie baza ↔ C#

Ważne — nazwy się różnią (snake_case w bazie, PascalCase w C#). Przy ręcznym SQL trzeba pamiętać:

| Baza (kolumna) | C# (property) | Uwagi |
|---|---|---|
| `id` | `Id` | |
| `name` | `Name` | |
| `sku` | `Sku` | |
| `category_id` | `CategoryId` | |
| `quantity` | `Quantity` | |
| `min_quantity` | `MinQuantity` | |
| `price` | `Price` | |
| `product_id` | `ProductId` | |
| `user_id` | `UserId` | **string, nie int!** |
| `type` | `Type` | "IN" / "OUT" |
| `created_at` | `CreatedAt` | |

**Przykład:** czytanie z `DataTable` w C#:
```csharp
// Jeśli SQL zwraca kolumnę 'category_id':
int catId = (int)row["category_id"];

// Jeśli chcemy mieć po polsku w widoku — używamy aliasu w SQL:
// "SELECT c.name AS category_name FROM..."
string catName = row["category_name"].ToString();
```

---

## 📝 Nazwy zmiennych w kodzie (konwencja)

### Lokalne zmienne i properties
**camelCase po angielsku, krótkie i jednoznaczne:**

✅ Dobre:
```csharp
var products = _db.Query("...");
var product = new Product();
int productId = 5;
string searchText = "śruba";
decimal totalPrice = 10.50m;
```

❌ Złe:
```csharp
var produkty = _db.Query("...");          // mix polski/angielski
var p = new Product();                     // za krótkie
int produktID = 5;                         // wielkie litery w złym miejscu
string tekst_szukania = "śruba";           // snake_case w C#
var d = 10.50m;                            // jednoliterowe
```

### Prywatne pola klas
**Prefiks `_` + camelCase:**

```csharp
private readonly Db _db;
private readonly UserManager<ApplicationUser> _userManager;
```

### Nazwy metod
**PascalCase, czasownik + rzeczownik:**

✅ Dobre: `GetProducts()`, `AddProduct()`, `DeleteCategory()`, `CheckStock()`
❌ Złe: `products()`, `doProduct()`, `p()`, `Zrob_Produkty()`

### Metody stron Razor (PageModel)
**Konwencja ASP.NET — nie zmieniać!**

- `OnGet()` — gdy ktoś wchodzi na stronę (GET)
- `OnPost()` — gdy wyśle formularz (POST)
- `OnGetStan()` — GET handler z nazwą "Stan" (używamy w raportach PDF: `asp-page-handler="Stan"`)
- `OnPostDelete()` — POST handler z nazwą "Delete"

---

## 🏷️ Nazwy dla `@page`, URL-i, linków

### Strony Razor (nazwa pliku = nazwa w URL)
| URL | Plik | Kto robi |
|---|---|---|
| `/` | `Pages/Index.cshtml` | Wojtek (dashboard) |
| `/Identity/Account/Login` | auto (Identity) | Wojtek (spolszcza) |
| `/Identity/Account/Register` | auto (Identity) | Wojtek (spolszcza) |
| `/Products` | `Pages/Products/Index.cshtml` | Adam |
| `/Products/Create` | `Pages/Products/Create.cshtml` | Adam |
| `/Products/Edit/5` | `Pages/Products/Edit.cshtml` | Adam |
| `/Products/Delete/5` | `Pages/Products/Delete.cshtml` | Adam |
| `/Stock` | `Pages/Stock/Index.cshtml` | Krzyś |
| `/Stock/Operation?type=IN` | `Pages/Stock/Operation.cshtml` | Krzyś |
| `/Stock/History` | `Pages/Stock/History.cshtml` | Krzyś |
| `/Reports` | `Pages/Reports/Index.cshtml` | Wojtek |

**Reguła:** w linkach Razor używajcie `asp-page="/Products/Index"` zamiast hardkodowania URL-a (`href="/Products"`). Inaczej jak Gabryś zmieni routing, wszystko pęknie.

### Nazwy handlerów (dla raportów)
```csharp
// W ReportsController:
public IActionResult OnGetStan() { ... }      // /Reports?handler=Stan
public IActionResult OnGetHistoria() { ... }  // /Reports?handler=Historia
```

W widoku:
```html
<a asp-page-handler="Stan">Pobierz raport stanu</a>
```

---

## 🎨 Nazwy w widokach (Razor .cshtml)

### Dla użytkownika — po polsku
```html
<h1>Produkty</h1>
<label>Nazwa produktu</label>
<button>Zapisz</button>
<th>Cena</th>
```

### W kodzie Razor — po angielsku
```html
@model Product
@{
    var categories = Model.Categories;
}

@foreach (var product in Model.Products) {
    <tr>
        <td>@product.Name</td>
    </tr>
}
```

### Properties modelu PageModel — po angielsku, ale jak się zdarzy polski — trzymajmy się konsekwencji w ramach jednej klasy

✅ Dobre (wszystko angielskie):
```csharp
public DataTable Products { get; set; }
public string SearchQuery { get; set; }
public int CategoryId { get; set; }
```

✅ OK (wszystko polskie - spójnie w klasie):
```csharp
public DataTable Produkty { get; set; }
public string Szukaj { get; set; }
public int KategoriaId { get; set; }
```

❌ Źle (mix):
```csharp
public DataTable Produkty { get; set; }   // polski
public string SearchQuery { get; set; }   // angielski
```

**Sugestia:** jako ekipa zdecydujcie raz — albo wszystko po angielsku (rekomendowane), albo wszystko po polsku. **Nie mieszajcie.**

---

## 🔑 Stałe wartości (magic strings)

Te rzeczy powinny być spójne w całym kodzie:

### Typy operacji
```csharp
// W operations.type
"IN"    // przyjęcie towaru
"OUT"   // wydanie towaru
```

**Nie używaj:** "in", "Out", "przyjecie", "wydanie" — tylko `"IN"` / `"OUT"`.

### Role użytkowników (Identity)
```csharp
"Admin"       // administrator
"Pracownik"   // zwykły użytkownik
```

### Klucze w TempData (flash messages)
```csharp
TempData["Success"]  // komunikaty sukcesu (zielony)
TempData["Error"]    // komunikaty błędów (czerwony)
TempData["Info"]     // informacje (niebieski)
```

### Nazwy Claim Types (Identity)
```csharp
User.FindFirstValue(ClaimTypes.NameIdentifier)   // ID usera (string)
User.Identity.Name                                // login usera
User.IsInRole("Admin")                            // sprawdzenie roli
```

---

## 📦 Namespace w C#

Wszystkie klasy w namespace `Magazyn`:

```csharp
namespace Magazyn.Models;
namespace Magazyn.Pages.Products;
namespace Magazyn.Data;
```

---

## ⚠️ Pułapki na które musicie uważać

### 1. `user_id` jest stringiem, nie intem!
Identity używa GUID-ów. To string, nie int.

```csharp
// ❌ Źle:
int userId = 5;
new MySqlParameter("@u", 5)

// ✅ Dobrze:
string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
new MySqlParameter("@u", userId)
```

### 2. Pola nullable
Niektóre kolumny mogą być NULL:
- `products.category_id` — może być null (produkt bez kategorii)

```csharp
// ❌ Źle:
int catId = (int)row["category_id"];  // rzuci wyjątkiem jak NULL

// ✅ Dobrze:
int? catId = row["category_id"] == DBNull.Value 
    ? null 
    : (int?)row["category_id"];
```

### 3. Alias w SQL vs property w modelu
Przy JOIN-ach dodajecie aliasy — klucz w `DataRow` to alias, nie oryginalna nazwa kolumny:

```csharp
// SQL:
"SELECT p.name, c.name AS category_name FROM products p LEFT JOIN categories c ..."

// Czytanie:
row["name"]           // nazwa produktu
row["category_name"]  // nazwa kategorii (alias)
// NIE: row["c.name"] albo row["categories.name"]
```

### 4. DateTime w parametrach
```csharp
// ❌ Źle (data jako string):
new MySqlParameter("@od", "2026-04-01")

// ✅ Dobrze (data jako DateTime):
new MySqlParameter("@od", DateTime.Parse("2026-04-01"))
// albo
new MySqlParameter("@od", someDateTime)
```

---

## 🗺️ Szybki cheatsheet

**Chcę dodać produkt:**
- Tabela: `products`
- Klasa: `Product`
- Pola: `Name`, `Sku`, `CategoryId`, `Quantity`, `MinQuantity`, `Price`

**Chcę zarejestrować operację magazynową:**
- Tabela: `operations`
- Klasa: `Operation`
- Typy: `"IN"` / `"OUT"`
- UserID to STRING z Identity

**Chcę dostać login zalogowanego usera:**
```csharp
User.Identity.Name  // login
User.FindFirstValue(ClaimTypes.NameIdentifier)  // ID (GUID string)
```

**Chcę sprawdzić czy user jest adminem:**
```csharp
User.IsInRole("Admin")
```

**Chcę pokazać flash message:**
```csharp
TempData["Success"] = "Produkt dodany!";
// potem RedirectToPage(...)
```

**Chcę zablokować stronę dla niezalogowanych:**
```csharp
[Authorize]
public class MyPageModel : PageModel { ... }
```

---

## 🆘 Pytania? Konflikty?

Jak nie wiesz jak coś nazwać — spytaj na grupowym czacie **zanim** napiszesz kod. Lepiej raz ustalić niż potem przepisywać w 5 miejscach.

Jeśli coś w tym słowniku trzeba zmienić — daj znać ekipie i zaktualizuj ten plik. **Jedno źródło prawdy**, nie własne wymysły w różnych miejscach.

---

*Ostatnia aktualizacja: kwiecień 2026*
