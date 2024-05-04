namespace ApplicationLayer;

public record ReportDto(
    string? DocumentNumber,
    string? PaymentMethod,
    string? CreationDate,
    string? TotalSale,
    string? Product,
    int? Quantity,
    string? Price,
    string? TotalPrice
);
