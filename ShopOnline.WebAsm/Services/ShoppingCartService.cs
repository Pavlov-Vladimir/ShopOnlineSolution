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
            // TODO Log exception
            throw;
        }
    }

    public async Task<CartItemDto?> DeleteItem(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"api/ShoppingCart/{id}");

            if (response.IsSuccessStatusCode)
            {               
                return await response.Content.ReadFromJsonAsync<CartItemDto>();
            }

            return default;
        }
        catch (Exception)
        {
            // TODO Log exception
            throw;
        }
    }

    public async Task<List<CartItemDto>?> GetItems(int userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/ShoppingCart/{userId}/GetItems");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return Enumerable.Empty<CartItemDto>().ToList();
                }
                return await response.Content.ReadFromJsonAsync<List<CartItemDto>>();
            }
            else
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http status: {response.StatusCode}, Message: {message}");
            }
        }
        catch (Exception)
        {
            // TODO Log exception
            throw;
        }
    }
}
