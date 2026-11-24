# CrossApp
Наскрізний проєкт з крос-платформного програмування. <br>
Предметна область: Бібліотека. Сутності: Book, BookCopy, Reader, Loan. <br>
Призначення: облік видач примірників книг читачам.
## Запуск
dotnet build <br>
dotnet run --project src/Cli -f net10.0
## Publish
dotnet publish src/Cli -c Release -r linux-x64/win-x64 --self-contained true/false
## Середовище
.NET SDK 10.0, Ubuntu 24.04 x64
## Solution
### Cli
Наша програма, саме її ми запсукаємо, вона залежить від Core
### Core
Бібліотека, вона створює функціонал, які Cli може використовувати
## RID
| RID | Режим | Розмір | Runtime |
|----------|----------|----------|----------|
| linux-x64  | --self-contained true  | 159 MB  | Не потрібен |
| linux-x64  | --self-contained false  | 304 KB  | Потрібен |
| win-x64  | --self-contained true  | 155 MB  | Не потрібен |
| win-x64  | --self-contained false  | 464 KB  | Потрібен |
## -p:PublishSingleFile=true
Розмір src/Cli/bin/Release/net10.0/linux-x64/ - 71МБ, стало набагато менше файлів
## -p:PublishTrimmed=true
Розмір src/Cli/bin/Release/net10.0/linux-x64/ - 24МБ, є попередження про те, що при використанні JsonSerializer.Serialize можуть виникнути проблеми через те, що він визначає типи під час виконання програми

## Лабораторна робота 3

Імпортер читає UTF-8 CSV з роздільником `;` і необов’язковим заголовком `id;isbn;title;year`. Кожна книга займає один рядок. Крапка з комою всередині поля не підтримується. Без аргументів CLI читає `data/sample.csv` з поточного каталогу. Для іншого файлу додайте `-- шлях/до/файлу.csv` до команди запуску. Ключі `--info` і `--json` виводять відомості про середовище.

[Звіт до лабораторної роботи](lab03.md)

Змішаний CSV підтримує рядки `B;id;isbn;title;year` для книг і `R;id;name` для читачів. JSON містить масив об’єктів із тими самими полями. Поле `type` має значення `B` або `R`, за замовчуванням запис вважається книгою.

```bash
dotnet run --project src/Cli -f net10.0 -- data/mixed.csv
dotnet run --project src/Cli -f net10.0 -- data/sample.json
```
