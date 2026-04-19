# System Zarządzania Magazynem

Aplikacja webowa do zarządzania magazynem w małym przedsiębiorstwie. Umożliwia ewidencję produktów, kategoryzację asortymentu, śledzenie stanów magazynowych, rejestrację operacji przyjęcia i wydania, generowanie raportów oraz administrację kontami użytkowników.

**Projekt zaliczeniowy** · Informatyka, semestr IV · aplikacja webowa · ASP.NET Core Razor Pages + MySQL

## Spis treści

- [Skład zespołu](#skład-zespołu)
- [Opis projektu](#opis-projektu)
- [Funkcjonalności](#funkcjonalności)
- [Technologie](#technologie)
- [Architektura](#architektura)
- [Struktura projektu](#struktura-projektu)
- [Schemat bazy danych](#schemat-bazy-danych)
- [Podział zadań w zespole](#podział-zadań-w-zespole)
- [Harmonogram](#harmonogram)
- [Uruchomienie projektu](#uruchomienie-projektu)
- [Widoki aplikacji](#widoki-aplikacji)

## Skład zespołu

| Imię i nazwisko | Zakres odpowiedzialności |
|---|---|
| Gabryś | Architektura aplikacji, uwierzytelnianie, panel administracyjny |
| Adam | Moduł produktów i kategorii |
| Krzyś | Moduł stanów magazynowych, operacji i historii |
| Wojtek | Interfejs użytkownika, dashboard, wyszukiwarka, raporty PDF |

## Opis projektu

System został zaprojektowany jako rozwiązanie do prowadzenia ewidencji magazynowej w małych przedsiębiorstwach. Aplikacja działa w architekturze klient-serwer z przeglądarką internetową jako klientem, co pozwala na jej uruchomienie na dowolnym urządzeniu w sieci lokalnej bez konieczności instalacji oprogramowania po stronie użytkownika końcowego.

Projekt realizowany jest przez czteroosobowy zespół w ramach przedmiotu programowanie obiektowe. Celem dydaktycznym jest praktyczne zastosowanie zasad programowania obiektowego, projektowania baz danych relacyjnych oraz pracy zespołowej z wykorzystaniem systemu kontroli wersji Git.

### Wymagania funkcjonalne

1. Uwierzytelnianie użytkowników z hashowaniem haseł
2. Zarządzanie produktami (dodawanie, edycja, usuwanie, przeglądanie)
3. Zarządzanie kategoriami produktów
4. Ewidencja stanów magazynowych z wyróżnieniem produktów poniżej stanu minimalnego
5. Rejestracja operacji przyjęcia (IN) i wydania (OUT) z aktualizacją stanów
6. Historia operacji z filtrowaniem po dacie, typie operacji i użytkowniku
7. Wyszukiwarka globalna produktów (po nazwie, kodzie SKU, kategorii)
8. Generowanie raportów w formacie PDF (stan magazynu, historia, alerty)
9. Alerty o produktach poniżej stanu minimalnego
10. Panel administracyjny do zarządzania kontami użytkowników

### Wymagania niefunkcjonalne

- Aplikacja webowa dostępna przez przeglądarkę
- Responsywny interfejs użytkownika (obsługa różnych rozdzielczości)
- Bezpieczne przechowywanie haseł (hashowanie BCrypt)
- Ochrona przed atakami SQL Injection (parametryzowane zapytania)
- Kontrola dostępu na poziomie ról użytkowników
- Obsługa polskich znaków diakrytycznych (UTF-8)

## Funkcjonalności

### Dla użytkowników (rola: pracownik)

- Przeglądanie stanów magazynowych
- Wyszukiwanie produktów
- Rejestrowanie operacji przyjęcia i wydania towaru
- Przeglądanie historii operacji
- Generowanie raportów PDF

### Dla administratorów (rola: admin)

Wszystkie funkcje pracownika oraz dodatkowo:

- Zarządzanie kontami użytkowników
- Dodawanie i edycja produktów
- Zarządzanie kategoriami
- Usuwanie rekordów z systemu

## Technologie

### Stack główny

| Technologia | Wersja | Zastosowanie |
|---|---|---|
| .NET | 10 | Platforma wykonawcza |
| ASP.NET Core Razor Pages | 10 | Framework webowy |
| C# | 13 | Język programowania |
| MySQL | 8.0+ | System bazodanowy |
| Bootstrap | 5 | Framework CSS |

### Biblioteki (NuGet)

| Pakiet | Zastosowanie |
|---|---|
| MySqlConnector | Komunikacja z bazą MySQL |
| BCrypt.Net-Next | Hashowanie haseł |
| QuestPDF | Generowanie dokumentów PDF |

### Narzędzia deweloperskie

- **Visual Studio 2022** lub **Visual Studio Code** — środowisko programistyczne
- **XAMPP** — lokalne środowisko MySQL
- **Git** + **GitHub** — system kontroli wersji
- **phpMyAdmin** — administracja bazą danych

### Uzasadnienie wyboru technologii

**ASP.NET Core Razor Pages** został wybrany ze względu na prostą strukturę (jedna strona = para plików `.cshtml` + `.cshtml.cs`), brak konieczności konfigurowania routingu oraz niski próg wejścia dla osób rozpoczynających pracę z platformą .NET. W porównaniu do pełnego MVC wymaga mniej abstrakcji, co czyni go odpowiednim dla aplikacji CRUD z ograniczoną liczbą widoków.

**MySqlConnector** wybrano zamiast Entity Framework Core z uwagi na cel dydaktyczny przedmiotu — praca z bazą danych wymaga pisania zapytań SQL, co pozwala na lepsze zrozumienie relacji bazodanowych i optymalizacji zapytań.

**BCrypt** jest algorytmem kryptograficznym zaprojektowanym specjalnie do hashowania haseł, odpornym na ataki typu rainbow table dzięki wbudowanej soli.

**QuestPDF** jest nowoczesną biblioteką z deklaratywnym API, znacznie prostszym w użyciu niż starsze rozwiązania (iTextSharp).

## Architektura

Aplikacja realizuje architekturę warstwową z wyraźnym podziałem odpowiedzialności:

```
┌─────────────────────────────────────────────┐
│      Przeglądarka użytkownika (klient)       │
└──────────────────┬──────────────────────────┘
                   │ HTTP/HTTPS
┌──────────────────▼──────────────────────────┐
│   Warstwa prezentacji (Razor Pages)         │
│   - widoki .cshtml                          │
│   - modele stron (PageModel)                │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│   Warstwa logiki aplikacji                  │
│   - walidacja danych                        │
│   - obsługa sesji                           │
│   - autoryzacja (atrybut RequireLogin)      │
└──────────────────┬──────────────────────────┘
                   │
┌──────────────────▼──────────────────────────┐
│   Warstwa dostępu do danych (Db.cs)         │
│   - zapytania parametryzowane               │
└──────────────────┬──────────────────────────┘
                   │ MySqlConnector
┌──────────────────▼──────────────────────────┐
│            Baza danych MySQL                 │
└─────────────────────────────────────────────┘
```

### Bezpieczeństwo

- Hasła przechowywane jako hashe BCrypt
- Wszystkie zapytania SQL wykorzystują parametry (ochrona przed SQL Injection)
- Sesje przechowywane po stronie serwera z ograniczonym czasem życia
- Ochrona stron wymagających logowania poprzez atrybut `[RequireLogin]`
- Sprawdzanie uprawnień administratora na poziomie akcji wymagających podwyższonych uprawnień

## Struktura projektu

```
Magazyn/
├── Program.cs                   — konfiguracja aplikacji
├── Db.cs                        — warstwa dostępu do danych
├── RequireLoginAttribute.cs     — filtr autoryzacji
├── appsettings.json             — konfiguracja (bez danych wrażliwych)
├── database.sql                 — skrypt inicjalizujący bazę
│
├── Models/                      — klasy modeli domenowych
│   ├── User.cs
│   ├── Product.cs
│   ├── Category.cs
│   └── Operation.cs
│
├── Pages/                       — widoki i logika stron
│   ├── Shared/
│   │   └── _Layout.cshtml       — szablon bazowy
│   ├── Index.cshtml             — dashboard
│   ├── Account/                 — logowanie i wylogowanie
│   ├── Admin/                   — panel administratora
│   ├── Products/                — zarządzanie produktami
│   ├── Categories/              — zarządzanie kategoriami
│   ├── Stock/                   — stany magazynowe i operacje
│   ├── Search/                  — wyszukiwarka
│   └── Reports/                 — generowanie raportów
│
└── wwwroot/                     — zasoby statyczne
    ├── css/
    ├── js/
    └── lib/                     — Bootstrap
```

## Schemat bazy danych

Baza danych składa się z czterech tabel w trzeciej postaci normalnej.

```
┌──────────────────┐         ┌──────────────────┐
│     users        │         │    categories    │
├──────────────────┤         ├──────────────────┤
│ id (PK)          │         │ id (PK)          │
│ login (UNIQUE)   │         │ name (UNIQUE)    │
│ password_hash    │         └─────────┬────────┘
│ role             │                   │
│ created_at       │                   │ 1:N
└────────┬─────────┘                   │
         │                    ┌────────▼─────────┐
         │ 1:N                │    products      │
         │                    ├──────────────────┤
         │                    │ id (PK)          │
         │                    │ name             │
         │                    │ sku (UNIQUE)     │
         │                    │ category_id (FK) │
         │                    │ quantity         │
         │                    │ min_quantity     │
         │                    │ unit             │
         │                    │ price            │
         │                    └────────┬─────────┘
         │                             │
         │                             │ 1:N
         │          ┌──────────────────▼───┐
         └─────────►│    operations        │
                    ├──────────────────────┤
                    │ id (PK)              │
                    │ product_id (FK)      │
                    │ user_id (FK)         │
                    │ type ('IN'/'OUT')    │
                    │ quantity             │
                    │ note                 │
                    │ created_at           │
                    └──────────────────────┘
```

### Opis tabel

**users** — konta użytkowników systemu z rolami (admin / pracownik) i bezpiecznie zahashowanymi hasłami.

**categories** — słownik kategorii produktów. Pozwala na grupowanie asortymentu.

**products** — rekordy produktów magazynowych. Każdy produkt ma unikalny kod SKU, jest przypisany do kategorii i posiada pola określające aktualny stan oraz próg minimalny.

**operations** — rejestr operacji magazynowych. Każda operacja przyjęcia lub wydania jest logowana wraz z identyfikatorem użytkownika wykonującego, co zapewnia pełną audytowalność.

## Podział zadań w zespole

Zastosowano podejście modułowe — każdy członek zespołu odpowiada za pełny pionowy wycinek funkcjonalności (od widoków po logikę dostępu do bazy danych), co minimalizuje konflikty w systemie kontroli wersji.

### Gabryś — fundamenty i administracja

- Konfiguracja projektu ASP.NET Core
- Projekt schematu bazy danych (`database.sql`)
- Implementacja warstwy dostępu do danych (`Db.cs`)
- Mechanizm uwierzytelniania z wykorzystaniem BCrypt
- Filtr autoryzacji (`RequireLoginAttribute`)
- Panel administracyjny zarządzania kontami

**Pliki:** `Program.cs`, `Db.cs`, `RequireLoginAttribute.cs`, `database.sql`, `Models/User.cs`, `Pages/Account/*`, `Pages/Admin/*`

### Adam — zarządzanie asortymentem

- Model produktów z walidacją danych wejściowych
- Interfejs przeglądania, dodawania, edycji i usuwania produktów
- System kategoryzacji produktów
- Walidacja unikalności kodu SKU
- Kolorowanie pozycji poniżej stanu minimalnego na liście

**Pliki:** `Models/Product.cs`, `Models/Category.cs`, `Pages/Products/*`, `Pages/Categories/*`

### Krzyś — operacje magazynowe

- Model operacji magazynowych
- Widok stanów magazynowych z wizualnym oznaczaniem alertów
- Formularz rejestracji przyjęcia i wydania towaru
- Walidacja dostępności towaru przy wydawaniu
- Historia operacji z filtrowaniem wielokryterialnym

**Pliki:** `Models/Operation.cs`, `Pages/Stock/*`

### Wojtek — interfejs i raportowanie

- Szablon bazowy aplikacji z nawigacją
- Dashboard ze statystykami i alertami
- Wyszukiwarka globalna
- Moduł generowania raportów PDF (3 typy raportów)
- Stylizacja interfejsu (CSS, Bootstrap)

**Pliki:** `Pages/Shared/_Layout.cshtml`, `Pages/Index.cshtml`, `Pages/Search/*`, `Pages/Reports/*`, `wwwroot/css/site.css`

## Harmonogram

Projekt realizowany w modelu iteracyjnym, z kamieniami milowymi na koniec każdego tygodnia.

| Tydzień | Cel | Rezultat |
|---|---|---|
| 1 | Fundamenty aplikacji | Działający projekt z bazą danych, systemem logowania i podstawowym layoutem |
| 2 | Implementacja modułów funkcjonalnych | Kompletne moduły produktów, kategorii i operacji magazynowych |
| 3 | Funkcjonalności zaawansowane | Dashboard, wyszukiwarka, generowanie raportów PDF |
| 4 | Integracja, testowanie, dokumentacja | Aplikacja gotowa do prezentacji |

### Szacowana pracochłonność

| Moduł | Osoba | Szacowany nakład (h) |
|---|---|---|
| Fundamenty + uwierzytelnianie + admin | Gabryś | ~25 |
| Produkty + kategorie | Adam | ~20 |
| Stany + operacje + historia | Krzyś | ~20 |
| Layout + dashboard + raporty | Wojtek | ~25 |
| **Razem** | | **~90** |

## Uruchomienie projektu

### Wymagania wstępne

- .NET 10 SDK
- MySQL 8.0 lub wyższy (np. XAMPP)
- Git

### Instalacja

```bash
# Klonowanie repozytorium
git clone https://github.com/<username>/Magazyn.git
cd Magazyn

# Konfiguracja bazy danych
# Uruchom skrypt database.sql w phpMyAdmin lub konsoli MySQL

# Konfiguracja połączenia
# Skopiuj appsettings.Development.example.json → appsettings.Development.json
# Uzupełnij connection string swoim hasłem do MySQL

# Instalacja zależności i uruchomienie
dotnet restore
dotnet run
```

Aplikacja będzie dostępna pod adresem `https://localhost:5001`.

### Domyślne konto administratora

| Login | Hasło |
|---|---|
| admin | admin123 |

Hasło należy zmienić po pierwszym logowaniu.

## Widoki aplikacji

Aplikacja składa się z jedenastu głównych widoków rozplanowanych zgodnie z przepływem użytkownika. Makiety poszczególnych ekranów dostępne są w pliku [`docs/mockups.html`](docs/mockups.html).

### Mapa przepływu użytkownika

```
    Logowanie
        │
        ▼
   ┌────────┐
   │ Dashboard │◄───────┐
   └────┬────┘          │
        │               │
   ┌────┴────┬────────┬─────────┬──────────┐
   ▼         ▼        ▼         ▼          ▼
Produkty  Stany  Historia  Wyszukaj   Raporty
   │         │
   ▼         ▼
Edycja   Operacja
         (IN/OUT)
```

### Lista widoków

1. **Logowanie** — uwierzytelnianie użytkownika
2. **Dashboard** — panel główny ze statystykami i alertami
3. **Lista produktów** — przeglądanie asortymentu z wyszukiwaniem
4. **Formularz produktu** — dodawanie i edycja produktu
5. **Kategorie** — zarządzanie kategoriami produktów
6. **Stany magazynowe** — aktualne stany z oznaczeniem alertów
7. **Operacja magazynowa** — rejestracja przyjęcia lub wydania
8. **Historia operacji** — dziennik operacji z filtrami
9. **Wyszukiwarka** — wyszukiwanie globalne po różnych kryteriach
10. **Raporty** — generowanie dokumentów PDF
11. **Panel administratora** — zarządzanie kontami użytkowników

## Licencja

Projekt realizowany w celach edukacyjnych w ramach zajęć na studiach informatycznych.

---

*Dokument ostatnio aktualizowany: kwiecień 2026*
