# OISISI projekat 2026/2027

Informacioni sistem uprave sajma knjiga. C#, MVC.

## Struktura

| Projekat        | Sadrzaj                                                  |
| --------------- | -------------------------------------------------------- |
| `Core`          | Modeli, servisi, DAO, interfejsi i helperi. Bez UI koda. |
| `ConsoleClient` | Konzolni interfejs: `Controllers`, `Program.cs`.         |

Obim: domaci 1, funkcionalnosti 8.1-8.4 (model, serijalizacija, CRUD i prikaz u konzoli).

## Pokretanje

```
dotnet build
dotnet run --project ConsoleClient
```

## Podela rada

- Bojan Konjevic: `#posetilac`, `#kupovina`, `#izdavac`.
- Luna Sabani: `#adresa`, `#autor`, `#knjiga`.
