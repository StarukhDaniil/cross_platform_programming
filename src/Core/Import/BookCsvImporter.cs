using System.Globalization;
using Core.Dto;

namespace Core.Import;

public static class BookCsvImporter
{
    private const char Separator = ';';

    public static ImportResult<CatalogEntryDto> Load(string path)
    {
        var items = new List<CatalogEntryDto>();
        var errors = new List<string>();
        int number = 0;
        bool first = true;
        foreach (string line in File.ReadLines(path))
        {
            number++;
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith('#'))
                continue;

            bool isHeader = first && line.Trim().Equals("id;isbn;title;year", StringComparison.OrdinalIgnoreCase);
            first = false;
            if (isHeader)
                continue;

            switch (ParseLine(line))
            {
                case Parsed book:
                    items.Add(book.Value);
                    break;
                case Failed error:
                    errors.Add($"рядок {number}: {error.Reason}");
                    break;
            }
        }

        return new(items.AsReadOnly(), errors.AsReadOnly());
    }

    private static ParseOutcome ParseLine(string line)
    {
        string[] fields = line.Split(Separator, StringSplitOptions.TrimEntries);
        return fields switch
        {
            ["B", .. var book] when fields.Length != 4 => ParseBook(book),
            ["R", "", _] or ["R", _, ""] => new Failed("порожній ідентифікатор або ім’я читача"),
            ["R", var id, var name] => new Parsed(new ReaderDto(id, name)),
            _ => ParseBook(fields)
        };
    }

    private static ParseOutcome ParseBook(string[] fields) =>
        fields switch
        {
            { Length: not 4 } => new Failed($"очікується 4 колонки, отримано {fields.Length}"),
            ["", _, _, _] => new Failed("порожній ідентифікатор"),
            [_, "", _, _] => new Failed("порожній ISBN"),
            [_, _, "", _] => new Failed("порожня назва книги"),
            [var id, var isbn, var title, var year]
                when int.TryParse(year, NumberStyles.None, CultureInfo.InvariantCulture, out int value)
                    && value > 0 => new Parsed(new BookDto(id, isbn, title, value)),
            _ => new Failed($"рік «{fields[3]}» має бути додатним цілим числом")
        };

    private abstract record ParseOutcome;
    private sealed record Parsed(CatalogEntryDto Value) : ParseOutcome;
    private sealed record Failed(string Reason) : ParseOutcome;
}
