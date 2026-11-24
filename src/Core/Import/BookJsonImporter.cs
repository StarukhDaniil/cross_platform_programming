using System.Text.Json;
using Core.Dto;

namespace Core.Import;

public static class BookJsonImporter
{
    public static ImportResult<CatalogEntryDto> Load(string path)
    {
        using var stream = File.OpenRead(path);
        using var document = JsonDocument.Parse(stream);
        if (document.RootElement.ValueKind != JsonValueKind.Array)
            throw new JsonException("очікується масив записів");

        var items = new List<CatalogEntryDto>();
        var errors = new List<string>();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        int number = 0;
        foreach (var element in document.RootElement.EnumerateArray())
        {
            number++;
            try
            {
                if (element.ValueKind != JsonValueKind.Object)
                    throw new JsonException("очікується об’єкт");

                CatalogEntryDto? entry = element.TryGetProperty("type", out var type) ? type.ToString() switch
                {
                    "R" => element.Deserialize<ReaderDto>(options),
                    "B" => element.Deserialize<BookDto>(options),
                    _ => throw new JsonException("невідомий тип запису")
                } : element.Deserialize<BookDto>(options);

                bool valid = entry switch
                {
                    BookDto book => !string.IsNullOrWhiteSpace(book.Isbn)
                        && !string.IsNullOrWhiteSpace(book.Title) && book.Year > 0,
                    ReaderDto reader => !string.IsNullOrWhiteSpace(reader.Name),
                    _ => false
                };
                if (entry is null || string.IsNullOrWhiteSpace(entry.Id) || !valid)
                    throw new JsonException("перевірте обов’язкові поля запису");
                items.Add(entry);
            }
            catch (JsonException ex)
            {
                errors.Add($"елемент {number}: {ex.Message}");
            }
        }

        return new(items.AsReadOnly(), errors.AsReadOnly());
    }
}
