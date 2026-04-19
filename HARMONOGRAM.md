# Magazyn — plan pracy dla ekipy

Co kto robi, kiedy, i co musi być gotowe przed czym. **10h na osobę, 4 tygodnie.**

## Zasady gry

1. **Każdy pracuje na swoim branchu** — `feature/gabrys-setup`, `feature/adam-produkty` itd.
2. **Pull Request do `main`** — przed merge'em ktoś inny musi spojrzeć (choćby na 2 minuty)
3. **Codziennie pull main przed pracą** — inaczej zrobisz sobie merge conflict
4. **Grupowy chat** — piszcie kiedy skończycie etap, bo ktoś może na Was czekać
5. **Jak coś nie działa >30 min** — pytaj ekipę, nie bij głową w ścianę

## Kolejność — co musi być zrobione przed czym

```
   TYDZIEŃ 1            TYDZIEŃ 2              TYDZIEŃ 3          TYDZIEŃ 4
   
   GABRYŚ              
   Setup Railway  ──┐
   dotnet new webapp│
   Identity + migr. │
   Db.cs            │
   database.sql     ├─► WSZYSCY KLONUJĄ I ODPALAJĄ
                    │
                    │   ADAM                    ADAM
                    │   Produkty CRUD           (poprawki)
                    │   (potrzebuje bazy)
                    │
                    │   KRZYŚ                   KRZYŚ
                    │   Stany + Operacje        Historia
                    │   (potrzebuje bazy       (potrzebuje
                    │    i produktów Adama)     operacji)
                    │
                    └─► WOJTEK                  WOJTEK          WSZYSCY
                        _Layout                 Dashboard       Testy
                        Strona logowania        Raport PDF      Prezentacja
                        (potrzebuje Identity)  (potrzebuje
                                                statystyk z bazy)
```

## Tydzień 1 — fundamenty (Gabryś pracuje sam, reszta się przygotowuje)

### Gabryś (10h) — **MUSI skończyć do końca tygodnia, reszta czeka**

**Dni 1-2 (~4h):**
- [ ] Konto na Railway, nowy projekt z MySQL, zapisz connection string
- [ ] `dotnet new webapp --auth Individual --name Magazyn`
- [ ] Zamiana SQLite na MySQL w `Program.cs` i `AppDbContext.cs`
- [ ] Paczki: `Pomelo.EntityFrameworkCore.MySql`, `MySqlConnector`, `QuestPDF`

**Dni 3-4 (~4h):**
- [ ] `dotnet ef migrations add Init` + `dotnet ef database update`
- [ ] Sprawdź w Railway/MySQL Workbench że tabele `AspNetUsers` się utworzyły
- [ ] Przetestuj rejestrację i logowanie (domyślne strony Identity)
- [ ] Napisz `database.sql` — tabele `categories`, `products`, `operations` + seed 4-5 kategorii
- [ ] Odpal `database.sql` na bazie Railway

**Dni 5-7 (~2h):**
- [ ] Napisz `Db.cs` — metody `Query()`, `Exec()`, `Scalar()`
- [ ] Zarejestruj `Db` w DI w `Program.cs`: `builder.Services.AddScoped<Db>();`
- [ ] Wrzuć projekt na GitHub
- [ ] Napisz w README jak podpiąć się do bazy (plik `appsettings.Development.example.json`)
- [ ] **Napisz do grupy: "Gotowe, klonujcie i działajcie"**

### Adam, Krzyś, Wojtek w tym tygodniu (~2h każdy, nauka)

**Oglądajcie tutoriale, uczcie się podstaw Razor Pages:**
- [ ] Microsoft Learn: [Get started with Razor Pages](https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/razor-pages-start)
- [ ] Sprawdźcie czym jest `PageModel`, `OnGet()`, `OnPost()`, `@page`, `[BindProperty]`
- [ ] **Gdy Gabryś skończy:** sklonujcie repo, odpalcie projekt lokalnie, zobaczcie że logowanie działa

---

## Tydzień 2 — Adam i Krzyś jadą, Wojtek zaczyna

### Adam (6h w tym tygodniu) — **Krzyś czeka na Twoje produkty**

**Dni 1-2 (~3h):**
- [ ] Model `Product.cs` z atrybutami walidacji (`[Required]`, `[Range]`, `[StringLength]`)
- [ ] `Pages/Products/Index.cshtml` + `.cs` — lista produktów z tabelą
- [ ] Zapytanie SQL JOIN z `categories` (żeby pokazać nazwę kategorii zamiast ID)

**Dni 3-5 (~3h):**
- [ ] `Pages/Products/Create.cshtml` — formularz dodawania z dropdownem kategorii
- [ ] Walidacja unikalności SKU w `OnPost()`
- [ ] **Napisz do grupy: "Produkty działają, można używać"**

### Krzyś (5h w tym tygodniu) — **zaczyna po Adamie (produkty muszą istnieć)**

**Dni 3-5 (~3h, po produkcie Adama):**
- [ ] Model `Operation.cs`
- [ ] `Pages/Stock/Index.cshtml` — lista stanów z kolorowaniem (czerwony jeśli `quantity < min_quantity`)

**Dni 6-7 (~2h):**
- [ ] `Pages/Stock/Operation.cshtml` — formularz IN/OUT
- [ ] Logika: walidacja przy OUT czy wystarczy, UPDATE products + INSERT operations

### Wojtek (4h w tym tygodniu)

**Dni 1-3 (~2h):**
- [ ] `Pages/Shared/_Layout.cshtml` — navbar Bootstrap z linkami do wszystkich modułów
- [ ] `_LoginPartial.cshtml` — pokazanie zalogowanego usera + link "Wyloguj"
- [ ] **Uzgodnij z ekipą nazwy ścieżek** — żeby link w Twoim layoucie działał z ich stronami

**Dni 4-7 (~2h):**
- [ ] Dostosuj wygląd `Areas/Identity/Pages/Account/Login.cshtml` i `Register.cshtml`
- [ ] Dodaj navbar do tych stron (domyślnie go nie mają)
- [ ] Sprawdź czy login/rejestracja wyglądają spójnie z resztą

---

## Tydzień 3 — dopełnienie

### Adam (3h w tym tygodniu)

- [ ] `Pages/Products/Edit.cshtml` — edycja produktu (użyj tego samego modelu co Create)
- [ ] `Pages/Products/Delete.cshtml` — potwierdzenie usunięcia
- [ ] Poprawki UX — flash message po zapisie ("Produkt dodany")

### Krzyś (4h w tym tygodniu)

**Dni 1-4 (~3h):**
- [ ] `Pages/Stock/History.cshtml` — lista operacji z JOIN na `products` i `AspNetUsers`
- [ ] Filtr po dacie (`[BindProperty(SupportsGet = true)]` dla `Od`, `Do`)

**Dni 5-7 (~1h):**
- [ ] Testy ręczne: przyjęcie, wydanie, sprawdź czy historia rośnie, czy stany się zgadzają

### Wojtek (5h w tym tygodniu) — **najważniejszy tydzień dla Ciebie**

**Dni 1-3 (~2h):**
- [ ] `Pages/Index.cshtml` — dashboard z 4 kafelkami:
  - Liczba produktów
  - Liczba kategorii
  - Liczba produktów poniżej minimum (alerty)
  - Liczba operacji dzisiaj
- [ ] Bootstrap cards z kolorowaniem (czerwony dla alertów)

**Dni 4-7 (~3h):**
- [ ] `Pages/Reports/Index.cshtml` — link do pobrania raportu
- [ ] Raport PDF stanu magazynu (QuestPDF) — tabela z nazwą, SKU, ilością, min, ceną
- [ ] Link w raporcie: `asp-page-handler="Stan"` → wywołuje `OnGetStan()` → zwraca `File()`

### Gabryś (2h w tym tygodniu) — wsparcie ekipy

- [ ] Odpowiada na pytania, pomaga z błędami
- [ ] Robi review PR-ów
- [ ] Jeśli coś w bazie nie działa — poprawia

---

## Tydzień 4 — integracja i oddanie

### Wszyscy razem (po ~2-3h każdy)

**Dni 1-3 — testowanie:**
- [ ] Wszyscy odpalają aplikację, każdy testuje scenariusz od początku do końca:
  1. Rejestracja nowego usera
  2. Logowanie
  3. Dodanie produktu
  4. Przyjęcie 50 szt.
  5. Wydanie 10 szt.
  6. Sprawdzenie historii
  7. Pobranie PDF
- [ ] Każdy znajduje i zgłasza bugi w swojej działce
- [ ] **Admin ustawiany ręcznie:** w MySQL Workbench wpisz jedną rolę do `AspNetRoles`, jedną przypiszcie do swojego konta

**Dni 4-5 — przygotowanie prezentacji:**
- [ ] Zrzuty ekranu do README (sekcja "Widoki aplikacji")
- [ ] Sprawdźcie czy README jest aktualny
- [ ] Kto prezentuje co na obronie — podzielcie się

**Dzień oddania:**
- [ ] Finalny commit i push
- [ ] Link do repo dla wykładowcy
- [ ] Aplikacja odpalona i gotowa do live demo

---

## Checklist "co musi działać przed oddaniem"

**Funkcjonalnie:**
- [ ] Rejestracja + logowanie + wylogowanie
- [ ] Dodanie produktu
- [ ] Edycja produktu
- [ ] Usunięcie produktu (z potwierdzeniem)
- [ ] Lista produktów z wyszukiwaniem
- [ ] Widok stanów magazynowych z kolorowaniem
- [ ] Przyjęcie towaru (IN)
- [ ] Wydanie towaru (OUT) z walidacją dostępności
- [ ] Historia operacji z filtrem daty
- [ ] Dashboard ze statystykami
- [ ] Raport PDF stanu magazynu

**Technicznie:**
- [ ] Hasła hashowane (Identity robi to samo, ale sprawdź w bazie że nie są plaintext)
- [ ] Wszystkie zapytania SQL parametryzowane (szukaj `"` + `"` w kodzie — nie powinno być)
- [ ] Żadnych hardcodowanych ścieżek (`C:\Users\...`)
- [ ] `appsettings.Development.json` w `.gitignore`
- [ ] README aktualny
- [ ] Projekt się buduje bez błędów (`dotnet build`)
- [ ] Projekt się odpala (`dotnet run`)

## Komunikacja — jak się dogadujemy

**Grupowy chat** (wybierzcie Discord / Messenger / cokolwiek):
- Informujcie kiedy skończycie etap
- Wklejajcie linki do PR-ów do review
- Pytajcie od razu jak coś nie działa (nie siedźcie godzinami sami)

**GitHub Issues** — dla bugów i zadań do zrobienia:
- Otwórzcie issue jak znajdziecie buga w czyjejś części
- Przypiszcie do odpowiedzialnej osoby

## Co robimy jak coś się wywali w ostatnim tygodniu

**Priorytet 1 — musi działać:**
- Logowanie + rejestracja
- CRUD produktów
- Operacje IN/OUT
- Stany magazynowe

**Priorytet 2 — miłe dodatki:**
- Historia z filtrami
- Raport PDF
- Dashboard

Jeśli coś z P2 nie zdąży się zrobić do końca — prezentujcie to co jest, nie męczcie się w ostatnim dniu z walczeniem o wszystko. Lepiej mieć 80% działające niż 100% rozpieprzone.

---

## Kontakt do siebie

Nazwisko / rola / kontakt (uzupełnijcie):
- **Gabryś** — setup, Identity, baza — tel/Discord:
- **Adam** — produkty — tel/Discord:
- **Krzyś** — magazyn, historia — tel/Discord:
- **Wojtek** — UI, dashboard, PDF — tel/Discord:

---

*Ostatnia aktualizacja: kwiecień 2026*

*Plan może się zmienić, ale dajcie znać ekipie przed zmianami.*
