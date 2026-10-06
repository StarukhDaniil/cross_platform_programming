# CrossApp
Наскрізний проєкт з крос-платформного програмування. <br>
Предметна область: Бібліотека. Сутності: Book, BookCopy, Reader, Loan. <br>
Призначення: облік видач примірників книг читачам.
## Запуск
dotnet build <br>
dotnet run --project src/Cli
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