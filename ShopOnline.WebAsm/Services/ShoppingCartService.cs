namespace ShopOnline.WebAsm.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public ShoppingCartService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CartItemDto?> AddItem(CartItemToAddDto cartItemToAddDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync<CartItemToAddDto>("api/ShoppingCart", cartItemToAddDto);
            
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return default;
                }
                return await response.Content.ReadFromJsonAsync<CartItemDto>();
            }
            else
            {
                var message = response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode}, Message: {message}"); 
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    public async Task<IEnumerable<CartItemDto>?> GetItems(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CartItemDto>();
                }
                return await response.Content.ReadFromJsonAsync<IEnumerable<CartItemDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}
