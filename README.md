# System Zarządzania Magazynem

Aplikacja webowa do zarządzania magazynem w małym przedsiębiorstwie. Umożliwia ewidencję produktów, śledzenie stanów magazynowych, rejestrację operacji przyjęcia i wydania oraz generowanie raportów.

**Projekt zaliczeniowy** · Informatyka, semestr IV · aplikacja webowa · ASP.NET Core Razor Pages + MySQL (Railway)

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
| Gabryś | Architektura aplikacji, uwierzytelnianie (Identity), schemat bazy, konfiguracja Railway |
| Adam | Moduł zarządzania produktami |
| Krzyś | Moduł operacji magazynowych i historii |
| Wojtek | Interfejs użytkownika, dashboard, raport PDF |

## Opis projektu

System został zaprojektowany jako rozwiązanie do prowadzenia ewidencji magazynowej w małych przedsiębiorstwach. Aplikacja działa w architekturze klient-serwer z przeglądarką internetową jako klientem. Baza danych hostowana jest w chmurze (Railway), dzięki czemu wszyscy członkowie zespołu pracują na wspólnym stanie danych, a wykładowca może zweryfikować działanie aplikacji w dowolnym momencie.

Projekt realizowany jest przez czteroosobowy zespół w ramach przedmiotu programowanie obiektowe. Celem dydaktycznym jest praktyczne zastosowanie zasad programowania obiektowego, projektowania relacyjnych baz danych, wykorzystania gotowych rozwiązań przemysłowych (ASP.NET Core Identity) oraz pracy zespołowej z systemem kontroli wersji Git.

### Wymagania funkcjonalne

1. Uwierzytelnianie użytkowników z wykorzystaniem ASP.NET Core Identity (logowanie, rejestracja)
2. Ewidencja produktów (dodawanie, edycja, usuwanie, przeglądanie)
3. Kategoryzacja produktów
4. Przegląd stanów magazynowych z wyróżnieniem pozycji poniżej stanu minimalnego
5. Rejestracja operacji przyjęcia (IN) i wydania (OUT) towaru
6. Historia operacji magazynowych
7. Generowanie raportu PDF ze stanem magazynu

### Wymagania niefunkcjonalne

- Aplikacja webowa dostępna przez przeglądarkę
- Responsywny interfejs użytkownika (Bootstrap 5)
- Bezpieczne przechowywanie haseł (ASP.NET Core Identity, PBKDF2)
- Ochrona przed atakami SQL Injection (parametryzowane zapytania)
- Baza danych hostowana w chmurze (Railway)
- Obsługa polskich znaków diakrytycznych (UTF-8)

## Funkcjonalności

### Logowanie i rejestracja

Każdy użytkownik tworzy sobie konto samodzielnie na stronie rejestracji. Uwierzytelnianie zapewnia ASP.NET Core Identity — gotowy komponent .NET, który obsługuje hashowanie haseł, sesje i ciasteczka. Rola administratora przyznawana jest ręcznie w bazie danych jednorazowo przy konfiguracji systemu.

### Zarządzanie produktami

Użytkownicy zalogowani mogą przeglądać listę produktów z możliwością wyszukiwania po nazwie lub kodzie SKU. Dodawanie, edycja i usuwanie produktów odbywa się przez dedykowane formularze z walidacją danych wejściowych. Każdy produkt jest przypisany do jednej z predefiniowanych kategorii.

### Stany magazynowe

Widok stanów pokazuje wszystkie produkty z aktualną ilością i stanem minimalnym. Pozycje, których ilość spadła poniżej minimum, są wyróżnione kolorystycznie (alert niskiego stanu).

### Operacje magazynowe

Rejestracja przyjęcia lub wydania towaru odbywa się przez formularz. Przy wydaniu system weryfikuje, czy dostępna ilość jest wystarczająca. Każda operacja jest logowana w historii wraz z identyfikatorem użytkownika wykonującego operację.

### Historia

Chronologiczna lista wszystkich operacji magazynowych z możliwością filtrowania po zakresie dat.

### Raport PDF

Generowanie raportu stanu magazynu w formacie PDF przy użyciu biblioteki QuestPDF.

## Technologie

### Stack główny

| Technologia | Wersja | Zastosowanie |
|---|---|---|
| .NET | 10 | Platforma wykonawcza |
| ASP.NET Core Razor Pages | 10 | Framework webowy |
| ASP.NET Core Identity | 10 | Uwierzytelnianie i autoryzacja |
| Entity Framework Core | 10 | ORM dla tabel Identity |
| C# | 13 | Język programowania |
| MySQL | 8.0 | System bazodanowy |
| Railway | — | Hosting bazy danych |
| Bootstrap | 5 | Framework CSS |

### Biblioteki (NuGet)

| Pakiet | Zastosowanie |
|---|---|
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | Identity z EF Core |
| Pomelo.EntityFrameworkCore.MySql | Provider MySQL dla EF Core |
| MySqlConnector | Bezpośrednie zapytania SQL do bazy |
| QuestPDF | Generowanie dokumentów PDF |

### Uzasadnienie wyboru technologii

**ASP.NET Core Razor Pages** wybrano ze względu na prostą strukturę (jedna strona = para plików `.cshtml` + `.cshtml.cs`) oraz brak konieczności konfigurowania routingu. Dla aplikacji CRUD o ograniczonej liczbie widoków jest to odpowiedniejsze rozwiązanie niż pełny model MVC.

**ASP.NET Core Identity** pozwala uniknąć samodzielnej implementacji logiki uwierzytelniania, która jest krytyczna dla bezpieczeństwa aplikacji. Wykorzystanie przetestowanego rozwiązania przemysłowego gwarantuje poprawne hashowanie haseł oraz zarządzanie sesjami.

**MySqlConnector** użyto dla tabel biznesowych (produkty, operacje) — pozwala na pisanie zapytań SQL, co realizuje cel dydaktyczny przedmiotu. Identity wymaga EF Core, więc dla tabel użytkowników użyto EF Core.

**Railway** jako hosting bazy pozwala całemu zespołowi pracować na wspólnych danych bez lokalnej instalacji MySQL u każdego członka. Darmowy plan Railway jest wystarczający dla projektu zaliczeniowego.

**QuestPDF** jest nowoczesną biblioteką z deklaratywnym API, znacznie prostszą w użyciu niż starsze alternatywy.

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
│   - ASP.NET Core Identity (uwierzytelnianie) │
│   - walidacja danych wejściowych            │
│   - atrybuty [Authorize]                    │
└─────────┬───────────────────┬───────────────┘
          │                   │
          │ EF Core           │ MySqlConnector
          │ (tabele Identity) │ (tabele biznesowe)
          ▼                   ▼
┌─────────────────────────────────────────────┐
│     Baza danych MySQL (Railway)              │
│     AspNetUsers, AspNetRoles                 │
│     products, categories, operations         │
└─────────────────────────────────────────────┘
```

## Struktura projektu

```
Magazyn/
├── Program.cs                   — konfiguracja aplikacji
├── Data/
│   └── AppDbContext.cs          — DbContext dla Identity (EF Core)
├── Db.cs                        — helper MySqlConnector dla tabel biznesowych
├── appsettings.json             — konfiguracja (bez danych wrażliwych)
├── database.sql                 — skrypt tworzący tabele biznesowe
│
├── Models/                      — klasy modeli domenowych
│   ├── ApplicationUser.cs       — model użytkownika (dziedziczy z IdentityUser)
│   ├── Product.cs
│   └── Operation.cs
│
├── Pages/                       — widoki i logika stron
│   ├── Shared/
│   │   ├── _Layout.cshtml       — szablon bazowy
│   │   └── _LoginPartial.cshtml — menu logowania
│   ├── Index.cshtml             — strona główna
│   ├── Products/                — CRUD produktów
│   │   ├── Index.cshtml         — lista
│   │   ├── Create.cshtml        — dodawanie
│   │   ├── Edit.cshtml          — edycja
│   │   └── Delete.cshtml        — usuwanie
│   ├── Stock/                   — stany i operacje
│   │   ├── Index.cshtml         — stany magazynowe
│   │   ├── Operation.cshtml     — przyjęcie/wydanie
│   │   └── History.cshtml       — historia
│   └── Reports/
│       └── Index.cshtml         — generowanie raportu PDF
│
└── Areas/Identity/              — strony Identity (logowanie, rejestracja)
    └── (generowane automatycznie)
```

## Schemat bazy danych

Baza danych składa się z tabel biznesowych oraz tabel użytkowników zarządzanych przez Identity.

```
┌────────────────┐            ┌──────────────────┐
│ AspNetUsers    │            │   categories     │
│ (Identity)     │            ├──────────────────┤
├────────────────┤            │ id (PK)          │
│ Id (PK)        │            │ name             │
│ UserName       │            └──────────┬───────┘
│ Email          │                       │
│ PasswordHash   │                       │ 1:N
│ ...            │                       │
└───────┬────────┘             ┌─────────▼────────┐
        │                      │    products      │
        │                      ├──────────────────┤
        │                      │ id (PK)          │
        │                      │ name             │
        │                      │ sku (UNIQUE)     │
        │                      │ category_id (FK) │
        │ 1:N                  │ quantity         │
        │                      │ min_quantity     │
        │                      │ price            │
        │                      └─────────┬────────┘
        │                                │
        │                                │ 1:N
        │            ┌───────────────────▼──┐
        └───────────►│    operations        │
                     ├──────────────────────┤
                     │ id (PK)              │
                     │ product_id (FK)      │
                     │ user_id (FK)         │
                     │ type ('IN'/'OUT')    │
                     │ quantity             │
                     │ created_at           │
                     └──────────────────────┘
```

### Opis tabel

**AspNetUsers** — tabela użytkowników systemu zarządzana przez ASP.NET Core Identity. Zawiera login, zahashowane hasło, adres e-mail oraz metadane sesji. Tworzona automatycznie przez migrację EF Core.

**categories** — słownik kategorii produktów. Zawiera podstawowe kategorie zdefiniowane przy inicjalizacji bazy.

**products** — ewidencja produktów magazynowych z unikalnym kodem SKU, przypisaniem do kategorii, aktualnym stanem oraz progiem minimalnym.

**operations** — rejestr operacji magazynowych. Każda operacja (przyjęcie lub wydanie) jest logowana z identyfikatorem użytkownika wykonującego, co zapewnia audytowalność systemu.

## Podział zadań w zespole

Zastosowano podejście modułowe — każdy członek zespołu odpowiada za pełny pionowy wycinek funkcjonalności, co minimalizuje konflikty w systemie kontroli wersji.

### Gabryś — fundamenty aplikacji (~10h)

- Utworzenie konta i projektu na Railway, konfiguracja bazy MySQL
- Inicjalizacja projektu ASP.NET Core z szablonu z Identity (`dotnet new webapp --auth Individual`)
- Konfiguracja `Program.cs`, `AppDbContext.cs` pod MySQL
- Utworzenie i zastosowanie migracji Identity (`dotnet ef migrations add Init`)
- Model `ApplicationUser.cs` (dziedziczy z `IdentityUser`)
- Skrypt `database.sql` tworzący tabele biznesowe (categories, products, operations) z seedem kategorii
- Helper `Db.cs` dla zapytań MySqlConnector
- Dokumentacja konfiguracji w README

**Kluczowe pliki:** `Program.cs`, `Data/AppDbContext.cs`, `Db.cs`, `Models/ApplicationUser.cs`, `database.sql`

### Adam — moduł produktów (~9h)

- Model `Product.cs` z atrybutami walidacji
- Strona listy produktów z wyszukiwaniem po nazwie i SKU
- Formularze dodawania i edycji produktu z walidacją unikalności SKU
- Strona potwierdzenia usunięcia produktu
- Dropdown kategorii w formularzach (pobierany z bazy)

**Kluczowe pliki:** `Models/Product.cs`, `Pages/Products/*.cshtml`

### Krzyś — moduł magazynowy (~9h)

- Model `Operation.cs`
- Strona stanów magazynowych z kolorowaniem pozycji poniżej minimum
- Formularz operacji przyjęcia/wydania z walidacją dostępności
- Implementacja logiki aktualizacji stanu po operacji (UPDATE + INSERT)
- Strona historii operacji z filtrem po dacie

**Kluczowe pliki:** `Models/Operation.cs`, `Pages/Stock/*.cshtml`

### Wojtek — interfejs, dashboard i raport (~10h)

- Szablon bazowy `_Layout.cshtml` z nawigacją
- Menu logowania w nagłówku (`_LoginPartial.cshtml`)
- Dostosowanie wyglądu stron Identity (logowanie, rejestracja) do spójnej stylistyki aplikacji
- Dashboard na stronie głównej — kafelki ze statystykami (liczba produktów, kategorii, produktów poniżej minimum) pobierane z bazy przez `Db.cs`
- Generowanie raportu PDF stanu magazynu (QuestPDF)
- Stylizacja CSS (drobne poprawki w `site.css`)

**Kluczowe pliki:** `Pages/Shared/_Layout.cshtml`, `Pages/Index.cshtml`, `Pages/Reports/Index.cshtml`, `Areas/Identity/Pages/Account/Login.cshtml`, `Areas/Identity/Pages/Account/Register.cshtml`, `wwwroot/css/site.css`

### Szacowana pracochłonność

| Moduł | Osoba | Szacowany nakład (h) |
|---|---|---|
| Fundamenty, Identity, Railway, baza | Gabryś | ~10 |
| Moduł produktów | Adam | ~9 |
| Moduł magazynowy | Krzyś | ~9 |
| Interfejs, dashboard, raport PDF | Wojtek | ~10 |
| **Razem** | | **~38** |

## Harmonogram

Projekt realizowany w modelu iteracyjnym z kamieniami milowymi na koniec każdego tygodnia.

| Tydzień | Cel | Rezultat |
|---|---|---|
| 1 | Fundamenty | Działający projekt z bazą na Railway, logowaniem, migracjami Identity |
| 2 | Moduły funkcjonalne | Produkty i operacje magazynowe działają end-to-end |
| 3 | Dopełnienie | Historia, raport PDF, layout, integracja |
| 4 | Testowanie i prezentacja | Aplikacja gotowa do oddania |

## Uruchomienie projektu

### Wymagania wstępne

- .NET 10 SDK
- Git
- Konto na Railway (darmowe) — tylko przy pierwszej konfiguracji bazy

### Instalacja

```bash
# Klonowanie repozytorium
git clone https://github.com/<username>/Magazyn.git
cd Magazyn

# Konfiguracja connection stringa
# Skopiuj appsettings.Development.example.json jako appsettings.Development.json
# Uzupełnij dane do bazy Railway (otrzymane od Gabrysia)

# Instalacja zależności
dotnet restore

# Zastosowanie migracji Identity (tylko pierwsze uruchomienie)
dotnet ef database update

# Uruchomienie skryptu database.sql
# (Otwórz w narzędziu MySQL Workbench / phpMyAdmin / DBeaver i wykonaj)

# Uruchomienie aplikacji
dotnet run
```

Aplikacja będzie dostępna pod adresem `https://localhost:5001`.

### Pierwsze logowanie

Przy pierwszym uruchomieniu należy:

1. Przejść na stronę rejestracji (`/Identity/Account/Register`)
2. Utworzyć konto administratora
3. (Jednorazowo) nadać rolę administratora w bazie przez bezpośrednie zapytanie SQL

## Widoki aplikacji

Aplikacja składa się z ośmiu głównych widoków. Szczegółowe makiety dostępne są w pliku [`docs/mockups.html`](docs/mockups.html).

### Mapa przepływu użytkownika

```
    Logowanie / Rejestracja
             │
             ▼
        Strona główna
             │
   ┌─────────┼─────────┬──────────┐
   ▼         ▼         ▼          ▼
Produkty   Stany   Historia   Raport
   │         │
   ▼         ▼
Formularz  Operacja
(dodaj/    (IN/OUT)
edytuj)
```

### Lista widoków

1. **Logowanie** (Identity) — uwierzytelnianie
2. **Rejestracja** (Identity) — zakładanie konta
3. **Strona główna** — powitanie i menu
4. **Lista produktów** — przeglądanie z wyszukiwaniem
5. **Formularz produktu** — dodawanie i edycja
6. **Stany magazynowe** — stany z oznaczeniem alertów
7. **Operacja magazynowa** — rejestracja przyjęcia/wydania
8. **Historia operacji** — dziennik z filtrem daty
9. **Raport PDF** — generowanie dokumentu

## Licencja

Projekt realizowany w celach edukacyjnych w ramach zajęć na studiach informatycznych.

---

*Dokument ostatnio aktualizowany: kwiecień 2026*
