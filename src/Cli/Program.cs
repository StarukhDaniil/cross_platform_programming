using System.Text.Encodings.Web;
using System.Text.Json;
using Core;
using Core.Import;

if (!args.Contains("--json") && !args.Contains("--info"))
{
    string path = args.FirstOrDefault() ?? Path.Combine("data", "sample.csv");
    try
    {
        var result = BookCsvImporter.Load(path);
        Console.WriteLine($"Завантажено книг: {result.Items.Count}");
        foreach (var book in result.Items.Take(5))
            Console.WriteLine($"  {book.Id}  {book.Isbn}  {book.Title} ({book.Year})");

        Console.WriteLine($"Пропущено рядків: {result.Errors.Count}");
        foreach (string error in result.Errors)
            Console.WriteLine($"  {error}");
        return 0;
    }
    catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
    {
        Console.Error.WriteLine($"Файл не знайдено: {Path.GetFullPath(path)}");
        return 1;
    }
    catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException)
    {
        Console.Error.WriteLine($"Не вдалося прочитати файл «{path}»: {ex.Message}");
        return 1;
    }
}

string student = "Старух Даніїл, ФЕІ-35";
string domain = "Бібліотека";

var report = EnvironmentInfo.Collect();

Console.WriteLine($"Build Note: {EnvironmentInfo.BuildNote}");

if (args.Contains("--json"))
{
    var info = new
    {
        Student = student,
        report.OsDescription,
        report.OsEnvironment,
        report.ProcessArchitecture,
        report.DotNetVersion,
        report.FrameworkDescription,
        report.BaseDirectory,
        report.DetectedRid,
        report.ReportedRid,
        Domain = domain
    };

    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    Console.WriteLine(JsonSerializer.Serialize(info, options));
}
else
{
    Console.WriteLine("CrossApp – практикум з крос-платформного програмування");
    Console.WriteLine($"Студент: {student}");
    Console.WriteLine(new string('-', 52));

    Console.WriteLine($"ОС (OSDescription)      : {report.OsDescription}");
    Console.WriteLine($"ОС (Environment)        : {report.OsEnvironment}");
    Console.WriteLine($"Архітектура процесу     : {report.ProcessArchitecture}");
    Console.WriteLine($"Версія .NET (CLR)       : {report.DotNetVersion}");
    Console.WriteLine($"Runtime                 : {report.FrameworkDescription}");
    Console.WriteLine($"Каталог застосунку      : {report.BaseDirectory}");

    Console.WriteLine(new string('-', 52));
    Console.WriteLine($"Предметна область: {domain}");
}
return 0;
