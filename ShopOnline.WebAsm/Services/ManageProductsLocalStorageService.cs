namespace ShopOnline.WebAsm.Services;

public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
{
    private const string LOCAL_STORAGE_KEY = "ProductCollection";

    private readonly ILocalStorageService _localStorageService;
    private readonly IProductService _productService;

    public ManageProductsLocalStorageService(ILocalStorageService localStorageService, IProductService productService)
    {
        _localStorageService = localStorageService;
        _productService = productService;
    }

    public async Task<IEnumerable<ProductDto>?> GetCollection()
    {
        return (await _localStorageService.GetItemAsync<IEnumerable<ProductDto>?>(LOCAL_STORAGE_KEY) ??
                await AddCollection());
    }

    public async Task RemoveCollection()
    {
        await _localStorageService.RemoveItemAsync(LOCAL_STORAGE_KEY);
    }

    private async Task<IEnumerable<ProductDto>?> AddCollection()
    {
        var productsCollection = await _productService.GetItems();

        if (productsCollection is not null)
        {
            await _localStorageService.SetItemAsync(LOCAL_STORAGE_KEY, productsCollection);
        }

        return productsCollection;
    }
}
