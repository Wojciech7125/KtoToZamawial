# System Zarządzania Magazynem

Aplikacja webowa do ewidencji produktów i operacji magazynowych. Projekt zaliczeniowy dla 4-osobowego zespołu.

## Zespół

| Osoba | Zakres |
|---|---|
| Gabryś | Architektura, Identity, baza danych |
| Adam | Moduł produktów |
| Krzyś | Moduł operacji magazynowych |
| Wojtek | Interfejs, dashboard, raport PDF |

## Funkcjonalności

- Rejestracja i logowanie użytkowników
- Dodawanie, edycja, usuwanie produktów
- Kategoryzacja produktów
- Przeglądanie stanów magazynowych z alertem dla produktów poniżej minimum
- Rejestracja operacji przyjęcia i wydania towaru
- Historia operacji z filtrowaniem po dacie
- Dashboard ze statystykami
- Generowanie raportu PDF stanu magazynu

## Technologie

- **.NET 10** + **ASP.NET Core Razor Pages**
- **MySQL** (hostowana na Railway)
- **ASP.NET Core Identity** + **Entity Framework Core** — uwierzytelnianie
- **MySqlConnector** — zapytania do tabel biznesowych
- **QuestPDF** — generowanie dokumentów PDF
- **Bootstrap 5** — interfejs użytkownika

## Schemat bazy danych

```
AspNetUsers (Identity)    categories
    │                          │
    │ 1:N                      │ 1:N
    │                          ▼
    │                      products
    │                          │
    │                          │ 1:N
    │                          ▼
    └─────────────────►   operations
                        (IN / OUT)
```

Tabele:
- `AspNetUsers` — użytkownicy (zarządzane przez Identity)
- `categories` — kategorie produktów
- `products` — produkty z ilościami i cenami
- `operations` — rejestr operacji magazynowych

## Uruchomienie

Wymagania: .NET 10 SDK, dostęp do bazy MySQL.

```bash
git clone <url-repo>
cd Magazyn
cp appsettings.Development.example.json appsettings.Development.json
# Uzupełnij connection string w appsettings.Development.json
dotnet restore
dotnet ef database update
dotnet run
```

Aplikacja będzie dostępna pod adresem `https://localhost:5001`.

Po uruchomieniu należy zarejestrować konto użytkownika przez stronę `/Identity/Account/Register`.

## Struktura projektu

```
Magazyn/
├── Program.cs              — konfiguracja aplikacji
├── Db.cs                   — dostęp do bazy (MySqlConnector)
├── Data/                   — kontekst EF Core dla Identity
├── Models/                 — klasy modeli (Product, Category, Operation)
├── Pages/                  — widoki Razor Pages
│   ├── Products/           — CRUD produktów
│   ├── Stock/              — stany i operacje magazynowe
│   └── Reports/            — generowanie raportu PDF
└── wwwroot/                — zasoby statyczne (CSS, JS)
```

## Widoki

1. Logowanie / Rejestracja
2. Dashboard z alertami niskich stanów
3. Lista produktów z wyszukiwaniem
4. Formularz dodawania i edycji produktu
5. Stany magazynowe z oznaczeniem alertów
6. Formularz operacji przyjęcia/wydania
7. Historia operacji z filtrem dat
8. Generowanie raportu PDF

## Dokumentacja

Makiety ekranów dostępne w pliku [`docs/mockups.html`](docs/mockups.html).

---

*Projekt realizowany w ramach zajęć na studiach informatycznych, semestr IV.*
