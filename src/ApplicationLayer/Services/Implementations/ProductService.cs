using DomainLayer.Models;
using InfrastructureLayer;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer;

public class ProductService : IProductService
{
    private readonly IGenericRepository<Product> _productRepository;

    public ProductService(IGenericRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductDto> Create(ProductDto newProductDto)
    {
        try
        {
            var product = await _productRepository.Create(newProductDto.ToProductEntity());

            if (product.Id == 0) throw new TaskCanceledException("No se pudo crear el producto");

            return product.ToProductDto();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var productFound = await _productRepository.GetById(id) ?? throw new TaskCanceledException("No se encontro el producto");

            await _productRepository.Delete(productFound.Id);

        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task Edit(ProductDto productDto)
    {
        try
        {
            var product = productDto.ToProductEntity();
            var productFound = await _productRepository.GetById(product.Id) ??
                throw new TaskCanceledException("No se encontro el producto");

            productFound.Name = product.Name;
            productFound.CategoryId = product.CategoryId;
            productFound.Stock = product.Stock;
            productFound.Price = product.Price;
            productFound.IsActive = product.IsActive;

            await _productRepository.Update(productFound);
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<List<ProductDto>> GetAll()
    {
        try
        {
            var productQuery = await _productRepository.GetAll();
            var products = productQuery.Include(p => p.Category).ToList();
            return products.Select(p => p.ToProductDto()).ToList();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
