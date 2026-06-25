# Słownik nazw — KTO TO ZAMAWIAŁ?

Żeby każdy używał tych samych nazw. Czytaj zanim cokolwiek nazwiesz.

**Zasada:** kod po angielsku, teksty dla użytkownika po polsku.

## Dane — plik `data/store.json`

Zamiast bazy danych mamy plik JSON. Struktura:

```json
{
  "categories": [ { "id": 1, "name": "Elektronika" } ],
  "products": [ { "id": 1, "name": "Mysz", "categoryId": 1,
                  "quantity": 45, "minQuantity": 10, "price": 59.99 } ],
  "operations": [ { "id": 1, "productId": 1, "userId": "1",
                    "type": "IN", "quantity": 50, "createdAt": "2026-05-18T09:12:00" } ]
}
```

## Klasy C# (Models/)

### `Category`
```csharp
public int Id { get; set; }
public string Name { get; set; }
```

### `Product`
```csharp
public int Id { get; set; }
public string Name { get; set; }
public int? CategoryId { get; set; }
public int Quantity { get; set; }
public int MinQuantity { get; set; }
public decimal Price { get; set; }
```

### `Operation`
```csharp
public int Id { get; set; }
public int ProductId { get; set; }
public string UserId { get; set; }   // string, nie int!
public string Type { get; set; }     // "IN" lub "OUT"
public int Quantity { get; set; }
public DateTime CreatedAt { get; set; }
```

## Klasa `Db` — jak używać

```csharp
// Wczytaj dane
var data = _db.Load();

// Pracuj na listach
data.Products    // List<Product>
data.Categories  // List<Category>
data.Operations  // List<Operation>

// Zapisz zmiany
_db.Save(data);
```

## Nazwy stron i URL

| URL | Plik | Kto |
|---|---|---|
| `/` | `Pages/Index.cshtml` | Gabryś + Wojtek |
| `/Account/Login` | `Pages/Account/Login.cshtml` | Gabryś |
| `/Products` | `Pages/Products/Index.cshtml` | Adam |
| `/Products/Create` | `Pages/Products/Create.cshtml` | Adam |
| `/Products/Edit/5` | `Pages/Products/Edit.cshtml` | Adam |
| `/Products/Delete/5` | `Pages/Products/Delete.cshtml` | Adam |
| `/Stock` | `Pages/Stock/Index.cshtml` | Krzyś |
| `/Stock/Operation` | `Pages/Stock/Operation.cshtml` | Krzyś |
| `/Stock/History` | `Pages/Stock/History.cshtml` | Krzyś |
| `/Users` | `Pages/Users/Index.cshtml` | Gabryś |
| `/Users/Create` | `Pages/Users/Create.cshtml` | Gabryś |
| `/Users/Edit/{id}` | `Pages/Users/Edit.cshtml` | Gabryś |
| `/Users/Delete/{id}` | `Pages/Users/Delete.cshtml` | Gabryś |

## Rangi użytkowników

Pole `"role"` w `data/users.json` określa uprawnienia:

| Ranga | Co widzi |
|---|---|
| `pracownik` | Produkty, Stany, Historia — brak panelu admina |
| `kierownik` | Wszystko + zakładka Użytkownicy (panel admina) |

Strony `/Users/*` są chronione atrybutem `[Authorize(Roles = "kierownik")]`.

## Stałe wartości

```csharp
"IN"   // przyjęcie towaru
"OUT"  // wydanie towaru
```

## Konwencja kodu

- Nazwy klas: PascalCase (`Product`, `Operation`)
- Properties: PascalCase (`Name`, `ProductId`)
- Zmienne lokalne: camelCase (`var products`, `int productId`)
- Prywatne pola: podkreślnik + camelCase (`_db`, `_cfg`)

## Pułapki

### UserId to string, nie int!
```csharp
// Dobrze:
string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

// Źle:
int userId = 5;
```

### Jak znaleźć nazwę kategorii produktu (nie ma JOIN!)
```csharp
var data = _db.Load();
var product = data.Products.FirstOrDefault(p => p.Id == id);
var category = data.Categories.FirstOrDefault(c => c.Id == product.CategoryId);
string catName = category?.Name ?? "-";
```

### Jak generować nowe ID
```csharp
product.Id = data.Products.Count > 0 ? data.Products.Max(p => p.Id) + 1 : 1;
```

### Strony wymagające logowania
```csharp
[Authorize]
public class MojaStronaModel : PageModel { }
```
