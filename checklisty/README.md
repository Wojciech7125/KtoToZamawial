# 📋 Checklisty projektu Magazyn

**4 osoby, 4 tygodnie, ~37h roboty total**

Każda osoba ma swój plik z checklistą:

- 👤 [GABRYS.md](./GABRYS.md) — fundamenty, Identity, baza (~10h)
- 👤 [ADAM.md](./ADAM.md) — produkty CRUD (~9h)
- 👤 [KRZYS.md](./KRZYS.md) — stany, operacje, historia (~9h)
- 👤 [WOJTEK.md](./WOJTEK.md) — layout, dashboard, PDF (~10h)

📖 **WAŻNE:** [SLOWNIK.md](./SLOWNIK.md) — nazwy tabel, kolumn, klas, pól. **Sprawdzajcie zanim cokolwiek nazwiecie** — inaczej każdy nazwie po swojemu i będzie bajzel.

## 🚦 Kolejność kto po kim

```
TYDZIEŃ 1          TYDZIEŃ 2           TYDZIEŃ 3          TYDZIEŃ 4
──────────         ──────────          ──────────         ──────────
  GABRYŚ                                                    WSZYSCY
  setup          ──►  ADAM       ──►   ADAM                 testy
  + baza              produkty          Edit/Delete         prezentacja
  + Identity                                               
                      KRZYŚ      ──►   KRZYŚ              
                      stany             historia          
                      + operacje                          
                                                          
                      WOJTEK     ──►   WOJTEK             
                      layout            dashboard         
                                        + PDF             
```

**Kluczowe zależności:**
- Tydzień 1: **Wszyscy czekają na Gabrysia** (setup + baza + migracje)
- Tydzień 2: **Krzyś czeka na Adama** (operacje potrzebują produktów)
- Tydzień 2: **Wojtek czeka na Gabrysia** (layout dziedziczy z Identity)
- Tydzień 3: **Wojtek czeka na Adama i Krzysia** (dashboard liczy z ich danych)

## 🤝 Zasady gry

### Git workflow
1. **Każdy ma swój branch:** `feature/twoje-imie-nazwa`
2. **Pull main przed pracą** (codziennie): `git pull origin main`
3. **Pull Request do main** — review przed merge (choćby 2 minuty)
4. **Commit często** — lepiej 5 małych niż 1 wielki
5. **Commit messages po polsku lub angielsku, byle konsekwentnie**

### Komunikacja
- **Grupowy chat** (Discord/Messenger/Slack) — codziennie rano co robicie
- **Skończyłeś etap?** Napisz do grupy, ktoś może na Ciebie czekać
- **Coś nie działa >30 minut?** Zapytaj ekipy, nie bij głową w ścianę
- **Znalazłeś bug w czyjejś części?** GitHub Issue + przypisanie do osoby

### Przed każdym PR
- [ ] Kod się buduje bez błędów (`dotnet build`)
- [ ] Odpaliłeś aplikację i przetestowałeś swoje zmiany (`dotnet run`)
- [ ] Żadnych hardcodowanych ścieżek typu `C:\Users\...`
- [ ] Żadnych zakomentowanych martwych kawałków kodu
- [ ] `appsettings.Development.json` NIE jest commitowany (ma być w `.gitignore`)

## 📋 Wspólna checklista przed oddaniem

### Funkcjonalnie musi działać:
- [ ] Rejestracja i logowanie (Gabryś + Wojtek)
- [ ] Dodanie, edycja, usunięcie produktu (Adam)
- [ ] Lista produktów z wyszukiwaniem (Adam)
- [ ] Widok stanów z kolorowaniem (Krzyś)
- [ ] Przyjęcie i wydanie towaru (Krzyś)
- [ ] Walidacja: nie można wydać więcej niż w magazynie (Krzyś)
- [ ] Historia operacji z filtrem daty (Krzyś)
- [ ] Dashboard ze statystykami (Wojtek)
- [ ] Raport PDF stanu magazynu (Wojtek)

### Technicznie:
- [ ] Wszystkie zapytania SQL parametryzowane (`@param`, nie sklejane ze stringów)
- [ ] Hasła hashowane przez Identity (nie plaintext w bazie — sprawdź!)
- [ ] `[Authorize]` na stronach wymagających logowania
- [ ] Projekt się buduje: `dotnet build`
- [ ] Projekt się odpala: `dotnet run`
- [ ] README.md aktualny

### Do prezentacji:
- [ ] Zrzuty ekranu każdego widoku w README
- [ ] Konto testowe dla wykładowcy (lub info "zarejestruj się przez Register")
- [ ] Link do repo wysłany wykładowcy
- [ ] Aplikacja odpalona i gotowa do demo

## 🆘 Plan B — jak coś w ostatnim tygodniu nie zdąży

### Priorytet 1 (bez tego nie ma zaliczenia):
1. Logowanie
2. Dodanie/lista produktów
3. Przyjęcie i wydanie
4. Stany magazynowe

### Priorytet 2 (miło mieć):
5. Edycja/usuwanie produktów
6. Historia
7. Dashboard
8. Raport PDF

**Jeśli P2 nie zdąży — prezentujcie co jest. Lepiej 80% działające niż 100% rozwalone.**

## 📞 Kontakt w ekipie

Uzupełnijcie:

| Osoba | Rola | Discord/Tel |
|---|---|---|
| Gabryś | Setup + Identity + baza | |
| Adam | Produkty | |
| Krzyś | Magazyn + historia | |
| Wojtek | UI + dashboard + PDF | |

## 🔗 Linki

- **Repo:** (Gabryś wstawi link)
- **Railway:** (Gabryś trzyma dostęp)
- **README:** [../README.md](../README.md)
- **Makiety:** [../docs/mockups.html](../docs/mockups.html)

---

*Powodzenia ekipo! Idźcie krok po kroku, róbcie mało ale systematycznie.*
