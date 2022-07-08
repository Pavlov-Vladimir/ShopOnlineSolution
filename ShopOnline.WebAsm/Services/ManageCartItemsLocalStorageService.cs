namespace ShopOnline.WebAsm.Services;

public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
{
    private const string LOCAL_STORAGE_KEY = "CartItemCollection";

    private readonly ILocalStorageService _localStorageService;
    private readonly IShoppingCartService _shoppingCartService;

    public ManageCartItemsLocalStorageService(ILocalStorageService localStorageService, IShoppingCartService shoppingCartService)
    {
        _localStorageService = localStorageService;
        _shoppingCartService = shoppingCartService;
    }

    public async Task<List<CartItemDto>?> GetCollection()
    {
        return (await _localStorageService.GetItemAsync<List<CartItemDto>?>(LOCAL_STORAGE_KEY) ??
                await AddCollection());
    }

    public async Task RemoveCollection()
    {
        await _localStorageService.RemoveItemAsync(LOCAL_STORAGE_KEY);
    }

    public async Task SaveCollection(List<CartItemDto>? cartItemDtos)
    {
        await _localStorageService.SetItemAsync(LOCAL_STORAGE_KEY, cartItemDtos);
    }

    private async Task<List<CartItemDto>?> AddCollection()
    {
        var cartItems = await _shoppingCartService.GetItems(HardCoded.UserId);

        if (cartItems is not null)
        {
            await _localStorageService.SetItemAsync(LOCAL_STORAGE_KEY, cartItems);
        }

        return cartItems;
    }
}
