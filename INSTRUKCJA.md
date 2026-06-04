# Jak odpalic

Wymaga: **.NET 10 SDK**

## Krok 1 - plik z userami

Musisz miec plik `data/users.json` (jest w `.gitignore` zeby nie wrzucac hasel
do gita). Stworz go recznie z taka zawartoscia:

```json
{
  "users": [
    { "id": "1", "username": "admin", "password": "admin" }
  ]
}
```

## Krok 2 - odpal

```
dotnet run
```

Przegladarka otworzy sie na http://localhost:5080 (albo cokolwiek wypisze).
Loguj sie userami z pliku users.json.

## Co dalej

- Produkty: dodawanie / edycja / usuwanie
- Stany: przyjecie / wydanie, walidacja zeby nie wydac wiecej niz na stanie
- Historia: filtr po dacie

Dane lecza do pliku `data/store.json` (kategorie, produkty, operacje).
