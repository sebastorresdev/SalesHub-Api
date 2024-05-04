using System.Linq.Expressions;
using DomainLayer;

namespace InfrastructureLayer;

public interface IGenericRepository<TModel> where TModel : EntityDB
{
    Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>>? filter = null);
    Task<TModel?> GetById(int id);
    Task<TModel?> GetFirstOrDefault(Expression<Func<TModel, bool>> filter);
    Task<TModel> Create(TModel value);
    Task Update(TModel value);
    Task Delete(int id);
}
