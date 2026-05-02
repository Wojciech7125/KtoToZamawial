# Checklista — Krzyś

**Zakres:** stany magazynowe, operacje IN/OUT, historia operacji
**Czas:** ~9h
**Ważne:** poczekaj aż Adam skończy produkty — operacje potrzebują produktów w `store.json`

## Jak zacząć

1. Sklonuj repo: `git clone https://github.com/Wojciech7125/KtoToZamawial.git`
2. Utwórz `data/users.json` (patrz README_EKIPA.md)
3. `dotnet run` — sprawdź czy logowanie działa
4. Utwórz swój branch: `git checkout -b feature/krzys-magazyn`

## Jak działają dane (zamiast bazy!)

Nie ma bazy danych. Są pliki JSON. Czytaj `GABRYS.md` — sekcja "Jak to działa".

W skrócie:
```csharp
var data = _db.Load();          // wczytaj
var products = data.Products;   // List<Product>
var ops = data.Operations;      // List<Operation>
_db.Save(data);                 // zapisz zmiany
```

## Etap 1: Widok stanów — `Pages/Stock/Index.cshtml`

Pliki już istnieją, uzupełnij widok:

### `Index.cshtml.cs` — już gotowe przez Gabrysia:
```csharp
public List<Product> Products { get; set; } = new();
public List<Category> Categories { get; set; } = new();

public void OnGet()
{
    var data = _db.Load();
    Products = data.Products;
    Categories = data.Categories;
}
```

### `Index.cshtml` — do uzupełnienia:
Tabela z kolumnami: Nazwa, Ilość, Minimum, Status

Kolorowanie wierszy:
```html
@foreach (var p in Model.Products)
{
    var status = p.Quantity < p.MinQuantity ? "NISKI" :
                 p.Quantity < (int)(p.MinQuantity * 1.2) ? "BLISKO" : "OK";
    var klasa = status == "NISKI" ? "table-danger" :
                status == "BLISKO" ? "table-warning" : "";
    <tr class="@klasa">
        <td>@p.Name</td>
        <td>@p.Quantity</td>
        <td>@p.MinQuantity</td>
        <td>@status</td>
    </tr>
}
```

## Etap 2: Operacje IN/OUT — `Pages/Stock/Operation.cshtml`

### `Operation.cshtml.cs` — już gotowe przez Gabrysia:
```csharp
public void OnGet() { Products = _db.Load().Products; }

public IActionResult OnPost()
{
    var data = _db.Load();
    var product = data.Products.FirstOrDefault(p => p.Id == ProductId);
    if (Type == "OUT" && product.Quantity < Quantity)
    { Error = "Za mało towaru na stanie."; Products = data.Products; return Page(); }

    if (Type == "IN") product.Quantity += Quantity;
    else product.Quantity -= Quantity;

    data.Operations.Add(new Operation {
        Id = data.Operations.Count > 0 ? data.Operations.Max(o => o.Id) + 1 : 1,
        ProductId = ProductId,
        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "?",
        Type = Type, Quantity = Quantity, CreatedAt = DateTime.Now
    });
    _db.Save(data);
    return RedirectToPage("Index");
}
```

### `Operation.cshtml` — do uzupełnienia:
Formularz z: radio IN/OUT, dropdown produktu, input ilości, przycisk zatwierdź.

## Etap 3: Historia — `Pages/Stock/History.cshtml`

### `History.cshtml.cs` — już gotowe przez Gabrysia:
```csharp
public void OnGet(DateTime? from, DateTime? to)
{
    var data = _db.Load();
    Products = data.Products;
    Operations = data.Operations
        .Where(o => (from == null || o.CreatedAt >= from) && (to == null || o.CreatedAt <= to))
        .OrderByDescending(o => o.CreatedAt).ToList();
}
```

### `History.cshtml` — do uzupełnienia:
Formularz filtrów (dwie daty), tabela operacji z kolumnami: Data, Produkt, Typ, Ilość.

Jak wyświetlić nazwę produktu (nie ma JOIN — szukasz po ID):
```html
@foreach (var o in Model.Operations)
{
    var prod = Model.Products.FirstOrDefault(p => p.Id == o.ProductId);
    <tr>
        <td>@o.CreatedAt</td>
        <td>@(prod?.Name ?? "-")</td>
        <td>@o.Type</td>
        <td>@o.Quantity</td>
    </tr>
}
```

## Checklist końcowa

- [ ] Widok stanów pokazuje wszystkie produkty
- [ ] Czerwony = poniżej minimum, żółty = blisko minimum
- [ ] Przyjęcie IN zwiększa stan i zapisuje do historii
- [ ] Wydanie OUT zmniejsza stan
- [ ] Nie można wydać więcej niż jest
- [ ] Historia pokazuje listę z filtrem po dacie
- [ ] `dotnet build` — zero błędów
- [ ] PR na GitHubie przed merge do main
