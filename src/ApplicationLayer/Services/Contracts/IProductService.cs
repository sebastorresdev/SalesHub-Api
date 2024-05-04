namespace ApplicationLayer;

public interface IProductService
{
    Task<List<ProductDto>> GetAll();
    Task<ProductDto> Create(ProductDto newProductDto);
    Task Edit(ProductDto productDto);
    Task Delete(int id);
}
