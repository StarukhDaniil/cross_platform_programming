namespace Core.Dto;

public abstract record CatalogEntryDto(string Id);

public sealed record ReaderDto(string Id, string Name) : CatalogEntryDto(Id);
