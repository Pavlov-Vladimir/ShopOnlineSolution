using ShopOnline.Models.Dtos;

namespace ShopOnline.WebAsm.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetItems();
}
