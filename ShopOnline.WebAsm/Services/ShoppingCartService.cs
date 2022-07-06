namespace ShopOnline.WebAsm.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly HttpClient _httpClient;

    public event Action<int>? OnShoppingCartChanged;

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

    public void RaiseEventOnShoppingCartChanged(int totalQty)
    {
        if (OnShoppingCartChanged is not null)
        {
            OnShoppingCartChanged.Invoke(totalQty);
        }
    }

    public async Task<CartItemDto?> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto)
    {
        try
        {
            var jsonRequest = JsonConvert.SerializeObject(cartItemQtyUpdateDto);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json-patch+json");

            var response = await _httpClient.PatchAsync($"api/ShoppingCart/{cartItemQtyUpdateDto.CartItemId}", content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CartItemDto>();
            }
            return null;
        }
        catch (Exception)
        {
            // Log exception
            throw;
        }
    }
}
