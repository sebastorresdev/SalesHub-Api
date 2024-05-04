namespace ApplicationLayer;

public record SalesDetailDto(
    int? ProductoId,
    string? ProductDescription,
    int? Quantity,
    string? Price,
    string? TotalPrice
);