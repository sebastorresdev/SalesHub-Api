namespace ApplicationLayer;

public record ProductDto(
    int Id,
    string? Name,
    int? CategoryId,
    string? CategoryDescription,
    int? Stock,
    string? Price,
    int IsActive
);
