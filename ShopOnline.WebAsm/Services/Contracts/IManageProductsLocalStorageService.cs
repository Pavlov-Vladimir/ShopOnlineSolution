namespace ShopOnline.WebAsm.Services.Contracts;

public interface IManageProductsLocalStorageService
{
    Task<IEnumerable<ProductDto>?> GetCollection();
    Task RemoveCollection();
}
