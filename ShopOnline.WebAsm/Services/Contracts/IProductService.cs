namespace ShopOnline.WebAsm.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>?> GetItems();
    Task<ProductDto?> GetItem(int id);
}
