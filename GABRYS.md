# 📋 Checklista — Gabryś

**Twój zakres:** fundamenty aplikacji, Identity, Railway, baza danych
**Szacowany czas:** ~10h
**Ważne:** robisz pierwszy, reszta czeka na Ciebie. Tydzień 1 to głównie Ty.

---

## 🚀 Etap 1: Railway i projekt (Tydzień 1, dzień 1-2) — ~4h

### 1.1 Konto na Railway
- [ ] Wejdź na [railway.app](https://railway.app) i załóż konto (przez GitHub najszybciej)
- [ ] Kliknij "New Project" → "Provision MySQL"
- [ ] Poczekaj 1-2 minuty aż się postawi
- [ ] Kliknij na usługę MySQL → zakładka **Variables** → skopiuj `MYSQL_URL` albo zbuduj connection string z: `MYSQLHOST`, `MYSQLPORT`, `MYSQLDATABASE`, `MYSQLUSER`, `MYSQLPASSWORD`
- [ ] Zapisz connection string w bezpiecznym miejscu (np. notatnik, potem wrzucisz do `appsettings.Development.json`)

**Format connection stringa dla .NET:**
```
Server=containers-us-west-XXX.railway.app;Port=7XXX;Database=railway;Uid=root;Pwd=TUTAJ_HASLO;
```

### 1.2 Nowy projekt .NET
- [ ] Otwórz terminal w folderze gdzie ma być projekt
- [ ] Wykonaj: `dotnet new webapp --auth Individual --name Magazyn`
- [ ] Wejdź do folderu: `cd Magazyn`
- [ ] Sprawdź że się odpala: `dotnet run` (powinno działać ze SQLite domyślnie)
- [ ] Zatrzymaj (Ctrl+C)

### 1.3 Paczki NuGet
```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package MySqlConnector
dotnet add package QuestPDF
```

- [ ] Zainstalowałem wszystkie 3 paczki
- [ ] Żadnych błędów? (jeśli są — zgłoś się do ekipy)

---

## 🗄️ Etap 2: Konfiguracja bazy (Tydzień 1, dzień 3-4) — ~4h

### 2.1 Connection string
- [ ] Otwórz `appsettings.json`
- [ ] Zmień `DefaultConnection` ze SQLite na MySQL Railway:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Port=...;Database=railway;Uid=root;Pwd=...;"
}
```
- [ ] **WAŻNE:** Dla bezpieczeństwa — skopiuj ten plik jako `appsettings.Development.json`, wrzuć prawdziwy connection string tam. Pilnuj żeby `appsettings.Development.json` był w `.gitignore`!

### 2.2 AppDbContext pod MySQL
- [ ] Otwórz `Data/ApplicationDbContext.cs` (albo się nazywa `AppDbContext.cs`)
- [ ] W `Program.cs` znajdź linijkę `builder.Services.AddDbContext<ApplicationDbContext>(...)` 
- [ ] Zamień ją na MySQL:
```csharp
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseMySql(connStr, ServerVersion.AutoDetect(connStr)));
```
- [ ] Sprawdzam czy się buduje: `dotnet build`

### 2.3 Migracje Identity
- [ ] Zainstaluj narzędzia EF (jeśli nie masz): `dotnet tool install --global dotnet-ef`
- [ ] Usuń starą migrację SQLite: `rm -r Migrations/` (lub usuń folder ręcznie)
- [ ] Stwórz nową: `dotnet ef migrations add Init`
- [ ] Zaaplikuj do bazy: `dotnet ef database update`
- [ ] **Sprawdź w Railway** (zakładka Data w panelu MySQL): czy tabele `AspNetUsers`, `AspNetRoles`, itd. się utworzyły

### 2.4 Test logowania
- [ ] Odpal: `dotnet run`
- [ ] Wejdź w przeglądarce → kliknij "Register" → załóż testowe konto
- [ ] Zaloguj się
- [ ] Sprawdź w Railway że Twoje konto jest w tabeli `AspNetUsers`

### 2.5 Skrypt `database.sql`
- [ ] Utwórz plik `database.sql` w głównym folderze projektu
- [ ] Wpisz SQL dla 3 tabel biznesowych:

```sql
-- Kategorie
CREATE TABLE categories (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) UNIQUE NOT NULL
);

-- Produkty
CREATE TABLE products (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(200) NOT NULL,
    sku VARCHAR(50) UNIQUE NOT NULL,
    category_id INT,
    quantity INT DEFAULT 0,
    min_quantity INT DEFAULT 0,
    price DECIMAL(10,2) DEFAULT 0,
    FOREIGN KEY (category_id) REFERENCES categories(id)
);

-- Operacje
CREATE TABLE operations (
    id INT AUTO_INCREMENT PRIMARY KEY,
    product_id INT NOT NULL,
    user_id VARCHAR(450) NOT NULL, -- Identity używa string GUID
    type ENUM('IN', 'OUT') NOT NULL,
    quantity INT NOT NULL,
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (product_id) REFERENCES products(id)
);

-- Przykładowe kategorie
INSERT INTO categories (name) VALUES 
  ('Elektronika'),
  ('Biuro'),
  ('Narzędzia'),
  ('Chemia');
```

- [ ] Odpal ten skrypt na bazie Railway
  - Opcja A: zakładka **Data** w Railway → "Query" → wklej i Run
  - Opcja B: MySQL Workbench → podłącz się → otwórz plik → Execute
- [ ] Sprawdź że tabele się utworzyły w Railway
- [ ] Sprawdź że są 4 kategorie: `SELECT * FROM categories;`

---

## 🔧 Etap 3: Helper Db.cs (Tydzień 1, dzień 5) — ~1h

### 3.1 Stwórz `Db.cs` w głównym folderze projektu
- [ ] Utwórz plik `Db.cs`
- [ ] Wpisz klasę helper (szczegóły w README, sekcja "Jak to zaprogramować"):

```csharp
using System.Data;
using MySqlConnector;

public class Db
{
    private readonly string _cs;
    
    public Db(IConfiguration cfg)
    {
        _cs = cfg.GetConnectionString("DefaultConnection");
    }
    
    public DataTable Query(string sql, params MySqlParameter[] p) { /* ... */ }
    public int Exec(string sql, params MySqlParameter[] p) { /* ... */ }
    public object Scalar(string sql, params MySqlParameter[] p) { /* ... */ }
}
```

### 3.2 Zarejestruj Db w DI
- [ ] W `Program.cs` dodaj przed `builder.Build()`:
```csharp
builder.Services.AddScoped<Db>();
```
- [ ] Dodaj konfigurację QuestPDF:
```csharp
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
```

### 3.3 Test że wszystko działa
- [ ] `dotnet build` — bez błędów?
- [ ] `dotnet run` — strona działa?
- [ ] Logowanie nadal działa?

---

## 📦 Etap 4: GitHub i dokumentacja (Tydzień 1, dzień 6-7) — ~1h

### 4.1 Repozytorium
- [ ] Utwórz nowe repo na GitHubie (prywatne, dodaj ekipę jako kolaboratorów)
- [ ] `.gitignore` — sprawdź czy `appsettings.Development.json` jest ignorowany (standardowy szablon .NET powinien to mieć, ale zweryfikuj!)
- [ ] Zrób pierwszy commit i push

### 4.2 Plik `appsettings.Development.example.json`
- [ ] Skopiuj swój `appsettings.Development.json` jako przykład bez hasła:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TUTAJ;Port=TUTAJ;Database=railway;Uid=root;Pwd=TUTAJ_HASLO;"
  }
}
```
- [ ] Ten plik commituj do repo (nie zawiera hasła)

### 4.3 Dokumentacja w README
- [ ] Sprawdź że README.md jest w repo (to który dla wykładowcy)
- [ ] Dodaj HARMONOGRAM.md jeśli go masz
- [ ] **Napisz do grupy na czacie: "Gotowe, klonujcie i działajcie! Pisałem do Was do głupio o co chodzi z connection stringiem, zobaczcie przykład w `appsettings.Development.example.json`"**

### 4.4 Wyślij connection string reszcie ekipy
- [ ] Prześlij prywatnie (DM, nie na publiczny czat!) connection string Adamowi, Krzysiowi, Wojtkowi
- [ ] **NIE** commituj connection stringa z hasłem do repo!

---

## ✅ Koniec tygodnia 1 — sprawdzenie

- [ ] Projekt na GitHubie
- [ ] Baza na Railway działa z tabelami Identity + biznesowymi
- [ ] Rejestracja i logowanie działają
- [ ] Każdy z ekipy może sklonować repo, wkleić connection string, odpalić `dotnet run`, i zalogować się
- [ ] Napisałem do ekipy że mogą zaczynać

---

## 🔁 Tygodnie 2-4: wsparcie ekipy (~2h w tygodniu 3)

### Tydzień 2 — mało roboty, jesteś dostępny
- [ ] Odpowiadaj na pytania na czacie (szybciej lepiej)
- [ ] Robisz review PR-ów od reszty ekipy
- [ ] Jeśli potrzeba — poprawiaj `Db.cs` albo schemat bazy

### Tydzień 3 — jeśli trzeba
- [ ] Mergujesz PR-y
- [ ] Pomagasz z błędami konfiguracji

### Tydzień 4 — prezentacja
- [ ] Testowanie ze wszystkimi
- [ ] Ręcznie nadaj sobie rolę admina (instrukcja niżej)
- [ ] Przygotowanie na obronę

**Jak zrobić sobie admina po rejestracji:**
```sql
-- 1. Dodaj rolę (jeśli nie ma)
INSERT INTO AspNetRoles (Id, Name, NormalizedName) 
VALUES (UUID(), 'Admin', 'ADMIN');

-- 2. Znajdź swoje UserId
SELECT Id, UserName FROM AspNetUsers;

-- 3. Znajdź Id roli Admin
SELECT Id, Name FROM AspNetRoles;

-- 4. Połącz (wklej swoje ID userów i ID roli)
INSERT INTO AspNetUserRoles (UserId, RoleId) 
VALUES ('TWOJE_USER_ID', 'ID_ROLI_ADMIN');
```

---

## ⚠️ Jeśli coś nie działa

**"dotnet ef not found":**
→ `dotnet tool install --global dotnet-ef`

**Błąd łączenia z Railway:**
→ Sprawdź czy connection string ma wszystkie pola (Server, Port, Database, Uid, Pwd)
→ Railway czasem restartuje serwer — poczekaj 2 min i spróbuj ponownie

**Migracje rzucają błędami:**
→ Usuń folder `Migrations/` i spróbuj ponownie od `dotnet ef migrations add Init`

**"Paczka Pomelo nie działa z .NET 10":**
→ Użyj `--prerelease` przy instalacji: `dotnet add package Pomelo.EntityFrameworkCore.MySql --prerelease`
