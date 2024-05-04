using System.Transactions;
using DomainLayer.Models;
using InfrastructureLayer.Data;

namespace InfrastructureLayer;

public class SaleRepository : GenericRepository<Sale>, ISaleRepository
{
    private const int LENGHT = 6;
    public SaleRepository(SalesHubDbContext dbContext) : base(dbContext)
    { }

    // INVESTIGAR PARA MEJORAR ESTE METODO
    public async Task<Sale> Register(Sale value)
    {
        var sale = new Sale();
        using var transaction = _dbContext.Database.BeginTransaction();

        try
        {
            foreach (var item in value.SalesDetails)
            {
                var product = await _dbContext.Products.FindAsync(item.Id) ??
                    throw new Exception($"No se encontró el producto con el ID {item.Id}");

                product.Stock -= item.Quantity;
                _dbContext.Products.Update(product);
            }

            await _dbContext.SaveChangesAsync();

            var documentNumber = _dbContext.DocumentNumbers.First();
            documentNumber.LastNumber++;
            documentNumber.CreationDate = DateTime.Now;

            _dbContext.DocumentNumbers.Update(documentNumber);

            await _dbContext.SaveChangesAsync();

            var saleNumber = documentNumber.LastNumber.ToString().PadLeft(LENGHT);

            value.DocumentNumber = saleNumber;

            await _dbContext.Sales.AddAsync(value);
            await _dbContext.SaveChangesAsync();

            sale = value;

            transaction.Commit();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            Console.WriteLine($"Error al registrar la venta: {e.Message}");
            throw;
        }
        return sale;
    }
}
