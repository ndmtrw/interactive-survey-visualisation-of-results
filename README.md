# Платформа за интерактивни анкети на живо и визуализация на резултати

## Връзки

- [GitHub хранилище](https://github.com/ndmtrw/interactive-survey-visualisation-of-results)
- [Документация](docs/README.md)

## Планирани функционалности

Създаване и провеждане на анкети, присъединяване чрез код или QR код, подаване на отговори и графична визуализация на резултатите в реално време.

## Стартиране на началния проект

Необходими са .NET 10 SDK и SQL Server Express LocalDB. От основната папка:

```powershell
dotnet restore
dotnet tool restore
dotnet ef database update --project src/SurveyLive
dotnet run --project src/SurveyLive
```

Адрес: [http://localhost:5080](http://localhost:5080). Връзката към SQL Server е в `src/SurveyLive/appsettings.json`.

## Автор

Atanas Dimitrov
