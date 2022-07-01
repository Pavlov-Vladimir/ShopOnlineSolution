using ShopOnline.Models.Dtos;
using ShopOnline.WebAsm.Services.Contracts;
using System.Net.Http.Json;

namespace ShopOnline.WebAsm.Services;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDto>> GetItems()
    {
        try
        {
            var products = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("api/Product");
            return products;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
