namespace ApplicationLayer;

public interface ISaleService
{
    Task<SaleDto> Register(SaleDto saledto);
    Task<List<SaleDto>> Historical(string search, string? saleNumber, string? startDate, string? endDate);
    Task<List<ReportDto>> Reports(string? startDate, string? endDate);
}
