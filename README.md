# Платформа за интерактивни анкети на живо и визуализация на резултати

Дипломен проект за уеб платформа за провеждане на интерактивни анкети с присъединяване чрез код или QR код и визуализация на резултатите в реално време.

## Текущ етап

Предварително проучване, документация, начална структура и първа версия на базата данни. Проектът е в начален етап, а планираните функционалности не са завършени.

## Връзки

- [GitHub хранилище](https://github.com/ndmtrw/interactive-survey-visualisation-of-results)
- [Документация](docs/README.md)
- [Кратко описание за предаване на етапа](docs/stage-1.md)

## Планирани функционалности

Водещият ще създава анкети с въпроси и варианти за отговор, ще стартира и приключва сесии и ще показва резултатите. Участниците ще се присъединяват чрез кратък код или QR код и ще отговарят през браузър без регистрация. Броят и процентът на отговорите ще се обновяват в реално време.

## Технологии

C#, ASP.NET Core MVC с Razor (.NET 10), Identity, Entity Framework Core Code First, MS SQL Server, HTML, CSS и JavaScript. За реалното време и графиките са планирани SignalR и Chart.js.

## Стартиране

Необходими са [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) и SQL Server. Настройката по подразбиране използва SQL Server Express LocalDB под Windows с Windows удостоверяване, без парола в проекта.

Изпълни от основната папка на хранилището:

```powershell
dotnet restore
dotnet tool restore
dotnet ef database update --project src/SurveyLive
dotnet run --project src/SurveyLive
```

Отвори [http://localhost:5080](http://localhost:5080). Показва се неактивна демонстрационна анкета с един въпрос и три варианта за отговор. Гласуване все още не е реализирано. Няма тестови профили, защото входът предстои.

Командата за миграция създава базата `SurveyLive`, петте основни таблици, Identity таблиците и началните данни: анкета, неактивна сесия, въпрос, три варианта и ролите Author, Presenter и Administrator. При повторно изпълнение вече приложената миграция се пропуска. Това е начална схема, без реализирани вход и управление на правата.

За друга SQL Server инстанция настрой `ConnectionStrings:DefaultConnection` чрез User Secrets от основната папка:

```powershell
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=SurveyLive;Trusted_Connection=True;TrustServerCertificate=True" --project src/SurveyLive
```

User Secrets се зареждат в среда Development. При такова заместване изпълни миграцията също в Development, например с `$env:ASPNETCORE_ENVIRONMENT = "Development"` преди командите. При публикуване използвай защитена настройка `ConnectionStrings__DefaultConnection` на сървъра. Доверието към локалния сертификат в примера е само за разработване.

## Структура

```text
src/SurveyLive/
  Controllers/     HTTP заявки за преглед на анкети
  Services/        Приложна логика чрез интерфейс и услуга
  Data/            EF Core контекст, Identity и ограничения
  Migrations/      Начална схема и демонстрационни данни
  Models/          Survey, SurveySession, Question, AnswerOption, Vote
  ViewModels/      Данни за представяне в интерфейса
  Views/           Начален Razor изглед
  Properties/      Настройки за локално стартиране
  wwwroot/         CSS и JavaScript
docs/              Концепция, увод, проучване и план
```

Началната схема е описана в [документацията](docs/README.md#5-начална-база-данни). `GET /api/surveys/1` връща демонстрационната анкета. Настройката за базата е в `src/SurveyLive/appsettings.json`.

## Автор

Atanas Dimitrov
