using System.Globalization;
using DomainLayer.Models;
using InfrastructureLayer;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer;

public class SaleService : ISaleService
{
    private readonly IGenericRepository<SalesDetail> _saleDetailRepository;
    private readonly ISaleRepository _saleRepository;

    public SaleService(IGenericRepository<SalesDetail> saleDetailRepository, ISaleRepository saleRepository)
    {
        _saleDetailRepository = saleDetailRepository;
        _saleRepository = saleRepository;
    }

    public async Task<List<SaleDto>> Historical(string search, string? saleNumber, string? startDate, string? endDate)
    {
        IQueryable<Sale> query = await _saleRepository.GetAll();
        var sales = new List<Sale>();
        try
        {
            if (search == "fecha")
            {
                if (!DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-PE"), DateTimeStyles.None, out DateTime startDateTime) ||
                    !DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-PE"), DateTimeStyles.None, out DateTime endDateTime))
                {
                    // Manejar el caso en que las fechas no son válidas
                    throw new ArgumentException("Las fechas proporcionadas no son válidas.");
                }

                sales = await query.Where(s =>
                                        s.CreationDate.HasValue &&
                                        s.CreationDate.Value.Date >= startDateTime &&
                                        s.CreationDate.Value.Date <= endDateTime)
                                   .Include(sd => sd.SalesDetails)
                                   .ThenInclude(p => p.Product)
                                   .ToListAsync();
            }
            else
            {
                sales = await query.Where(s =>
                                        s.DocumentNumber == saleNumber)
                                   .Include(sd => sd.SalesDetails)
                                   .ThenInclude(p => p.Product)
                                   .ToListAsync();
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Error al recuperar ventas históricas.", ex);
        }

        return sales.Select(s => s.ToSaleDto()).ToList();
    }

    public async Task<SaleDto> Register(SaleDto saledto)
    {
        try
        {
            var newSale = await _saleRepository.Register(saledto.ToSaleEntity());

            if (newSale.Id == 0) throw new TaskCanceledException("No se pudo crear");

            return newSale.ToSaleDto();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<ReportDto>> Reports(string? startDate, string? endDate)
    {
        IQueryable<SalesDetail> query = await _saleDetailRepository.GetAll();
        var salesDetail = new List<SalesDetail>();
        try
        {
            if (!DateTime.TryParseExact(startDate, "dd/MM/yyyy", new CultureInfo("es-PE"), DateTimeStyles.None, out DateTime startDateTime) ||
                    !DateTime.TryParseExact(endDate, "dd/MM/yyyy", new CultureInfo("es-PE"), DateTimeStyles.None, out DateTime endDateTime))
            {
                // Manejar el caso en que las fechas no son válidas
                throw new ArgumentException("Las fechas proporcionadas no son válidas.");
            }

            salesDetail = await query.Include(s => s.Product)
                                     .Include(s => s.Sale)
                                     .Where(sd => sd.Sale != null &&
                                                  sd.Sale.CreationDate.HasValue &&
                                                  sd.Sale.CreationDate.Value.Date >= startDateTime &&
                                                  sd.Sale.CreationDate.Value.Date <= endDateTime)
                                     .ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
        return salesDetail.Select(sd => sd.ToReportDto()).ToList();
    }
}
