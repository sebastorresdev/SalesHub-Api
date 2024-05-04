namespace ApplicationLayer;

public record SaleDto(
    int Id,
    string? DocumentNumber,
    string? PaymentMethod,
    string? TotalPrice,
    string? CreationDate,
    ICollection<SalesDetailDto> SalesDetails
);
