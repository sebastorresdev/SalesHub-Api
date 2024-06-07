
using System.Globalization;
using DomainLayer.Models;
using InfrastructureLayer;

namespace ApplicationLayer;

public class DashboardService : IDashboardService
{
    private readonly IGenericRepository<Product> _productRepository;
    private readonly ISaleRepository _saleRepository;

    public DashboardService(IGenericRepository<Product> productRepository, ISaleRepository saleRepository)
    {
        _productRepository = productRepository;
        _saleRepository = saleRepository;
    }

    private IQueryable<Sale> GetSales(IQueryable<Sale> saleTable, int totalDays)
    {
        var lastDate = saleTable.OrderByDescending(s => s.CreationDate)
                                .Select(s => s.CreationDate)
                                .FirstOrDefault();

        if (lastDate is null)
            throw new Exception("No se encontró la fecha más reciente de creación de ventas.");

        lastDate = lastDate.Value.AddDays(-totalDays);

        return saleTable.Where(s => s.CreationDate != null && s.CreationDate.Value.Date >= lastDate.Value.Date);
    }

    private async Task<int> TotalSalesLastWeek()
    {
        int total = 0;
        IQueryable<Sale> query = await _saleRepository.GetAll();

        if (query.Any())
        {
            var saleTable = GetSales(query, 7);
            total = saleTable.Count();
        }

        return total;
    }

    private async Task<string> TotalIncomeLastWeek()
    {
        decimal result = 0.0m;
        IQueryable<Sale> query = await _saleRepository.GetAll();

        if (query.Any())
        {
            var saleTable = GetSales(query, 7);
            result = saleTable.Select(s => s.TotalPrice ?? 0.0m)
                              .Sum(p => p);
        }

        return Convert.ToString(result, new CultureInfo("es-PE"));
    }

    private async Task<int> TotalProducts() =>
        (await _productRepository.GetAll()).Count();

    private async Task<Dictionary<string, int>> SaleLastWeek()
    {
        var result = new Dictionary<string, int>();
        var query = await _saleRepository.GetAll();
        if (query.Any())
        {
            var saleTable = GetSales(query, 7);
            result = saleTable.GroupBy(s => s.CreationDate != null ? s.CreationDate.Value.Date : DateTime.Now.Date) // verificar si toma encuenta la hora
                              .OrderBy(g => g.Key)
                              .Select(sd => new { date = sd.Key.ToString("dd/MM/yyyy"), total = sd.Count() })
                              .ToDictionary(r => r.date, r => r.total);
        }

        return result;
    }

    public async Task<DashboardDto> Summary()
    {
        try
        {
            // Mejorar esta llamada a los metodos asincronos
            var totalSales = await TotalSalesLastWeek();
            var totalIncome = await TotalIncomeLastWeek();
            var totalProducts = await TotalProducts();

            var WeeklySale = new List<WeeklySaleDto>();

            foreach (var item in await SaleLastWeek())
            {
                WeeklySale.Add(new WeeklySaleDto(item.Key, item.Value));
            }

            return new DashboardDto(totalSales, totalIncome, totalProducts, WeeklySale);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }
}
