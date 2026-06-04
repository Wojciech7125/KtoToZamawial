# KTO TO ZAMAWIAŁ?

Aplikacja magazynowa — projekt zaliczeniowy, 2. rok informatyki.

## Jak odpalić

1. Sklonuj repo: `git clone https://github.com/Wojciech7125/KtoToZamawial.git`
2. Wejdź do folderu: `cd magazyn-mysql`
3. Odpal: `dotnet run`
4. Otwórz w przeglądarce: `http://localhost:5080`
5. Zaloguj się: login `gabriel`, hasło `1234`

## Stack

- .NET 10 Razor Pages
- Dane w plikach JSON (`data/store.json`)
- Bootstrap 5 (z CDN)

## Funkcje

- Logowanie (bez rejestracji — jeden użytkownik w `data/users.json`)
- Produkty: lista z wyszukiwaniem, dodawanie, edycja, usuwanie
- Stany magazynowe z kolorowaniem (czerwony = poniżej minimum)
- Przyjęcie i wydanie towaru (walidacja: nie wydasz więcej niż masz)
- Historia operacji z filtrem po dacie
- Panel ze statystykami na stronie głównej

## Dane

Dane trzymane są w dwóch plikach JSON:
- `data/store.json` — produkty, kategorie, operacje (commitowany do repo)
- `data/users.json` — dane logowania (NIE commitowany — dodaj ręcznie)

Zawartość `data/users.json`:
```json
{
  "users": [
    { "id": "1", "username": "gabriel", "password": "1234" }
  ]
}
```

## Ekipa

- Gabryś — setup, JSON, logowanie, strona główna
- Adam — produkty (CRUD)
- Krzyś — stany, operacje IN/OUT, historia
- Wojtek — layout, navbar, Bootstrap
