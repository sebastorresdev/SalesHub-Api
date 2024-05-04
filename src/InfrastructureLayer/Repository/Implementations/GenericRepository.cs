
using System.Linq.Expressions;
using DomainLayer;
using InfrastructureLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace InfrastructureLayer;

public class GenericRepository<TModel> : IGenericRepository<TModel> where TModel : EntityDB
{
    protected readonly SalesHubDbContext _dbContext;
    protected DbSet<TModel> Entity => _dbContext.Set<TModel>();

    public GenericRepository(SalesHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TModel> Create(TModel value)
    {
        try
        {
            Entity.Add(value);
            await _dbContext.SaveChangesAsync();
            return value;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var deletedEntity = await GetById(id);
            if (deletedEntity is not null) Entity.Remove(deletedEntity);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IQueryable<TModel>> GetAll(Expression<Func<TModel, bool>>? filter = null) =>
        filter is null ? await Task.FromResult(Entity) : await Task.FromResult(Entity.Where(filter));


    public async Task<TModel?> GetById(int id) => await Entity.FirstOrDefaultAsync(e => e.Id == id);

    public Task<TModel?> GetFirstOrDefault(Expression<Func<TModel, bool>> filter) =>
        Entity.FirstOrDefaultAsync(filter);

    public async Task Update(TModel value)
    {
        try
        {
            Entity.Update(value);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}
