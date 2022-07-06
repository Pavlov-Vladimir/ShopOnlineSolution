namespace ShopOnline.WebAsm.Services.Contracts;

public interface IShoppingCartService
{
    Task<CartItemDto?> AddItem(CartItemToAddDto cartItemToAddDto);
    Task<List<CartItemDto>?> GetItems(int userId);
    Task<CartItemDto?> DeleteItem(int id);
    Task<CartItemDto?> UpdateQty(CartItemQtyUpdateDto cartItemQtyUpdateDto);

    event Action<int> OnShoppingCartChanged;
    void RaiseEventOnShoppingCartChanged(int totalQty);
}
