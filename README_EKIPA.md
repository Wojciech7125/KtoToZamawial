# KTO TO ZAMAWIAŁ? — info dla ekipy

Plik dla nas, nie dla wykładowcy.

## Co gdzie jest

- `README.md` — publiczna wersja dla wykładowcy
- `README_EKIPA.md` — ten plik
- `HARMONOGRAM.md` — kto co kiedy robi
- `SLOWNIK.md` — nazwy klas, pól, plików — czytać przed kodowaniem
- `mocups.html` — makiety ekranów

## Jak uruchomić projekt

1. Sklonuj repo
2. Utwórz ręcznie plik `data/users.json` (nie ma go w repo bo zawiera hasła):
```json
{
  "users": [
    { "id": "1", "username": "gabriel", "password": "1234" }
  ]
}
```
3. `dotnet run`

## Twoja checklista

- [GABRYS.md](./GABRYS.md)
- [ADAM.md](./ADAM.md) — to jest plik GABRYS.md (Adam ma swoje zadania opisane tam)
- [KRZYS.md](./KRZYS.md)
- [WOJTEK.md](./WOJTEK.md)

## Zasady

1. Każdy na swoim branchu: `feature/imie-nazwa`
2. Pull main rano przed pracą: `git pull origin main`
3. PR do main — ktoś musi zerknąć przed merge
4. Coś nie działa >30 min? Pytaj na grupowym chacie
5. `data/users.json` nigdy do repo — jest w `.gitignore`

## Kolejność
Tydzień 1:  Gabryś robi setup → reszta czeka
Tydzień 2:  Adam (produkty) → Krzyś (operacje, potrzebuje produktów)
Wojtek robi layout
Tydzień 3:  Adam (Edit/Delete), Krzyś (historia), Wojtek (navbar)
Tydzień 4:  Testy + prezentacja

## Co musi działać przed oddaniem

- Logowanie
- Dodawanie produktów
- Przyjęcie/wydanie towaru
- Stany magazynowe z kolorowaniem

## Kontakty

| Osoba | Rola | Kontakt |
|---|---|---|
| Gabryś | setup, JSON, logowanie | |
| Adam | produkty | |
| Krzyś | magazyn, historia | |
| Wojtek | UI, layout | |

## Linki

- Repo: https://github.com/Wojciech7125/KtoToZamawial
