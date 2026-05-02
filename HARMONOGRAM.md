# Harmonogram — KTO TO ZAMAWIAŁ?

## Podział pracy

| Osoba | Zakres | Branch |
|---|---|---|
| Gabryś | setup, JSON, logowanie, strona główna | `main` (już zrobione) |
| Adam | produkty CRUD | `feature/adam-produkty` |
| Krzyś | stany, operacje, historia | `feature/krzys-magazyn` |
| Wojtek | layout, navbar, Bootstrap | `feature/wojtek-layout` |

## Kolejność
Tydzień 1 — GABRYŚ (już zrobione):
setup .NET 10, JSON zamiast bazy, logowanie, strona główna, push na GitHub
Tydzień 2 — ADAM + WOJTEK równolegle:
Adam: lista produktów, dodawanie
Wojtek: navbar, layout
Tydzień 2/3 — KRZYŚ (po Adamie):
stany magazynowe, operacje IN/OUT, historia
Tydzień 3 — ADAM cd.:
edycja i usuwanie produktów
Tydzień 4 — WSZYSCY:
testy, prezentacja

## Zależności

- Krzyś czeka na Adama (operacje potrzebują produktów)
- Wojtek może zacząć od razu (layout niezależny)

## Git workflow

1. `git clone https://github.com/Wojciech7125/KtoToZamawial.git`
2. `git checkout -b feature/twoje-imie`
3. Pracuj, commituj często
4. `git push origin feature/twoje-imie`
5. Otwórz Pull Request na GitHubie → ktoś zerka → merge

## Przed oddaniem musi działać

- [ ] Logowanie
- [ ] Lista i dodawanie produktów
- [ ] Przyjęcie i wydanie towaru
- [ ] Stany z kolorowaniem
- [ ] Historia operacji
- [ ] `dotnet build` bez błędów
