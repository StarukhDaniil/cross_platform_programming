namespace Core.Dto;

public sealed record BookDto(string Id, string Isbn, string Title, int Year) : CatalogEntryDto(Id);
