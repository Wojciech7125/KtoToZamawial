# Checklista — Wojtek

**Zakres:** layout, navbar, Bootstrap, strona główna (wygląd)
**Czas:** ~6h

## Jak zacząć

1. Sklonuj repo: `git clone https://github.com/Wojciech7125/KtoToZamawial.git`
2. Utwórz `data/users.json` (patrz README_EKIPA.md)
3. `dotnet run` — sprawdź czy aplikacja działa
4. Utwórz swój branch: `git checkout -b feature/wojtek-layout`

## Co już jest zrobione

Layout bazowy (`Pages/Shared/_Layout.cshtml`) już istnieje z podstawowym navbarem i Bootstrapem 5. Twoje zadanie to go ulepszyć.

## Etap 1: Navbar

Plik: `Pages/Shared/_Layout.cshtml`

Navbar powinien mieć linki do:
- Strona główna (`/Index`)
- Produkty (`/Products/Index`)
- Stany (`/Stock/Index`)
- Historia (`/Stock/History`)
- Login/Wyloguj (już jest w `_LoginPartial.cshtml`)

Przykład linku w Razor:
```html
<a class="nav-link" asp-page="/Products/Index">Produkty</a>
```

## Etap 2: Strona główna

Plik: `Pages/Index.cshtml`

Strona główna już pokazuje statystyki (TotalProducts, LowStock, TotalOperations) i tabelę produktów z niskim stanem. Możesz ją ulepszyć wizualnie — Bootstrap karty, ikony, kolory.

Dostępne dane w `Model`:
```csharp
Model.TotalProducts    // int — liczba produktów
Model.LowStock         // int — produkty poniżej minimum
Model.TotalOperations  // int — liczba operacji
Model.Products         // List<Product> — wszystkie produkty
```

## Etap 3: Style

Plik: `wwwroot/css/site.css`

Możesz dodać własne style. Bootstrap jest ładowany z CDN więc nie musisz nic instalować.

## Ważne zasady

- Nie zmieniaj plików `.cshtml.cs` — tylko widoki `.cshtml`
- Linki rób przez `asp-page` nie przez `href` na sztywno
- Testuj po każdej zmianie: `dotnet run`

## Checklist końcowa

- [ ] Navbar ma wszystkie linki
- [ ] Strona główna wygląda czytelnie
- [ ] Aplikacja działa na telefonie (Bootstrap responsive)
- [ ] `dotnet build` — zero błędów
- [ ] PR na GitHubie przed merge do main
