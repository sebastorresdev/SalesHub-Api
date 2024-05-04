namespace ApplicationLayer;

public record DashboardDto(
    int TotalSales,
    string? TotalRevenue,
    int TotalProducts,
    List<WeeklySaleDto> LastWeeksSales
);
