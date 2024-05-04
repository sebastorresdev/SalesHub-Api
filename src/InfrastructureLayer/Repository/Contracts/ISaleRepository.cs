using DomainLayer.Models;

namespace InfrastructureLayer;

public interface ISaleRepository : IGenericRepository<Sale>
{
    Task<Sale> Register(Sale value);
}
