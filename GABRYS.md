# Checklista — Gabryś

**Zakres:** setup projektu, dane JSON, logowanie, strona główna
**Czas:** ~6h (już zrobione!)

## Co już zrobiłem

- [x] Projekt .NET 10 Razor Pages
- [x] Dane w plikach JSON zamiast bazy danych
- [x] Logowanie przez cookie (bez rejestracji)
- [x] Jeden użytkownik w `data/users.json`
- [x] Strona główna z podstawowymi statystykami
- [x] Layout z Bootstrapem, nazwa "KTO TO ZAMAWIAŁ?"
- [x] Push na GitHub

## Jak to działa (dla ekipy)

### Dane — klasa `Db`

Zamiast bazy danych używamy pliku `data/store.json`. Klasa `Db` w `Db.cs` ma dwie metody:

```csharp
// Wczytaj wszystkie dane
var data = _db.Load();

// Zapisz wszystkie dane
_db.Save(data);
```

`data` to obiekt `StoreData` z trzema listami:
```csharp
data.Products    // List<Product>
data.Categories  // List<Category>
data.Operations  // List<Operation>
```

### Przykład — jak dodać produkt (Adam)

```csharp
var data = _db.Load();
product.Id = data.Products.Count > 0 ? data.Products.Max(p => p.Id) + 1 : 1;
data.Products.Add(product);
_db.Save(data);
```

### Przykład — jak pobrać listę produktów (Adam)

```csharp
var data = _db.Load();
var products = data.Products; // List<Product>
```

### Przykład — jak dodać operację (Krzyś)

```csharp
var data = _db.Load();
var product = data.Products.FirstOrDefault(p => p.Id == productId);
product.Quantity += quantity; // IN
var op = new Operation {
    Id = data.Operations.Max(o => o.Id) + 1,
    ProductId = productId,
    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
    Type = "IN",
    Quantity = quantity,
    CreatedAt = DateTime.Now
};
data.Operations.Add(op);
_db.Save(data);
```

### Logowanie

Logowanie sprawdza `data/users.json`. Ten plik nie jest w repo — każdy tworzy go ręcznie:
```json
{
  "users": [
    { "id": "1", "username": "gabriel", "password": "1234" }
  ]
}
```

Strony wymagające logowania mają atrybut `[Authorize]` na klasie PageModel.

## Modele (Models/)

```csharp
// Category.cs
public class Category { public int Id; public string Name; }

// Product.cs
public class Product { public int Id; public string Name; public string Sku;
    public int? CategoryId; public int Quantity; public int MinQuantity; public decimal Price; }

// Operation.cs
public class Operation { public int Id; public int ProductId; public string UserId;
    public string Type; public int Quantity; public DateTime CreatedAt; }
```

## Struktura plików

```
Pages/
  Account/Login.cshtml(.cs)    — logowanie
  Account/Logout.cshtml(.cs)   — wylogowanie
  Index.cshtml(.cs)             — strona główna, statystyki
  Products/                     — Adam
  Stock/                        — Krzyś
  Shared/_Layout.cshtml         — Wojtek (navbar)
data/
  store.json                    — produkty, kategorie, operacje
  users.json                    — NIE commitować!
Db.cs                           — odczyt/zapis JSON
Program.cs                      — konfiguracja aplikacji
```
