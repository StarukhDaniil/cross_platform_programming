using System.Text.Encodings.Web;
using System.Text.Json;
using Core;
using Core.Import;
using Core.Dto;

string path = args.FirstOrDefault() ?? Path.Combine("data", "sample.csv");
try
{
    var result = Path.GetExtension(path).ToLowerInvariant() switch
    {
        ".csv" => BookCsvImporter.Load(path),
        ".json" => BookJsonImporter.Load(path),
        _ => throw new ArgumentException("підтримуються файли .csv та .json")
    };
    int readers = result.Items.OfType<ReaderDto>().Count();
    Console.WriteLine($"Завантажено книг: {result.Items.Count - readers}");
    if (readers > 0)
        Console.WriteLine($"Завантажено читачів: {readers}");
    foreach (var entry in result.Items.Take(5))
        Console.WriteLine(entry switch
        {
            BookDto book => $"  {book.Id}  {book.Isbn}  {book.Title} ({book.Year})",
            ReaderDto reader => $"  {reader.Id}  {reader.Name}",
            _ => entry.ToString()
        });

    int total = result.Items.Count + result.Errors.Count;
    double percent = total == 0 ? 0 : 100.0 * result.Errors.Count / total;
    Console.WriteLine($"Усього: {total} | Прийнято: {result.Items.Count} | Пропущено: {result.Errors.Count} | Помилки: {percent:F2}%");
    foreach (string error in result.Errors)
        Console.WriteLine($"  {error}");
    return 0;
}
catch (Exception ex) when (ex is FileNotFoundException or DirectoryNotFoundException)
{
    Console.Error.WriteLine($"Файл не знайдено: {Path.GetFullPath(path)}");
    return 1;
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or ArgumentException or JsonException)
{
    Console.Error.WriteLine($"Не вдалося прочитати файл «{path}»: {ex.Message}");
    return 1;
}
