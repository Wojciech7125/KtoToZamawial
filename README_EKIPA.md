# Magazyn — info dla ekipy

Plik dla nas, nie dla wykładowcy. Tutaj wszystko praktyczne — co, gdzie, jak.

## Co gdzie jest

- `README.md` — **publiczna wersja dla wykładowcy**, nie piszcie tam wewnętrznych rzeczy
- `README_EKIPA.md` — ten plik, dla nas
- `HARMONOGRAM.md` — kto co kiedy robi
- `checklisty/` — osobne listy zadań dla każdego z nas
  - `SLOWNIK.md` — **WAŻNE**, nazwy tabel/klas/pól, czytać przed kodowaniem
- `docs/mockups.html` — makiety ekranów

## Twoja checklista

Każdy klika swój plik:
- [GABRYS](./checklisty/GABRYS.md)
- [ADAM](./checklisty/ADAM.md)
- [KRZYS](./checklisty/KRZYS.md)
- [WOJTEK](./checklisty/WOJTEK.md)

## Zasady

1. **Każdy na swoim branchu** — `feature/imie-nazwa`
2. **Pull main rano** przed pracą
3. **PR do main**, ktoś musi zerknąć przed merge
4. **Coś nie działa >30 min?** Pytaj na grupowym chacie
5. **`SLOWNIK.md` przeczytać przed pisaniem kodu** — nazwy muszą być spójne

## Kolejność

```
Tydzień 1:  Gabryś robi setup → reszta czeka
Tydzień 2:  Adam (produkty) → Krzyś (operacje, potrzebuje produktów)
            Wojtek robi layout
Tydzień 3:  Adam (Edit/Delete), Krzyś (historia), Wojtek (dashboard + PDF)
Tydzień 4:  Testy + prezentacja
```

**Blokujące zależności:**
- Wszyscy czekają na Gabrysia w tygodniu 1
- Krzyś czeka na Adama (operacje potrzebują produktów)
- Wojtek czeka na Adama/Krzysia (dashboard liczy statystyki)

## Co na pewno musi działać przed oddaniem

- Logowanie
- Dodawanie produktów
- Przyjęcie/wydanie towaru
- Stany magazynowe z kolorowaniem

Reszta (edycja, historia, PDF, dashboard) — miło mieć, ale priorytet niższy.

## Dostępy

- **GitHub repo:** (wstawić link)
- **Railway:** Gabryś ma dostęp, connection string wyśle prywatnie
- **Grupowy chat:** (Discord/Messenger — wybrać)

## Kontakty

| Osoba | Rola | Kontakt |
|---|---|---|
| Gabryś | setup, Identity, baza | |
| Adam | produkty | |
| Krzyś | magazyn, historia | |
| Wojtek | UI, dashboard, PDF | |

## Pułapki na które wszyscy musicie uważać

- **`appsettings.Development.json` NIGDY do repo!** Gabryś wyśle prywatnie
- **SQL tylko z parametrami** — nigdy `"WHERE id=" + id`, zawsze `@id`
- **`user_id` to string** (Identity używa GUID-ów, nie intów)
- **Nazwy z `SLOWNIK.md`** — nie wymyślajcie własnych

## Jak coś wywali się w ostatnim tygodniu

Priorytet: zrób żeby działało minimum (logowanie + produkty + operacje), resztę wytnij. Lepiej 80% działające niż 100% rozwalone na prezentacji.

---

*Update: kwiecień 2026*
