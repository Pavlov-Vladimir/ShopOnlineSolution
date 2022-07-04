namespace ShopOnline.WebAsm.Services.Contracts;

public interface IShoppingCartService
{
    Task<CartItemDto?> AddItem(CartItemToAddDto cartItemToAddDto);
    Task<IEnumerable<CartItemDto>?> GetItems(int userId);
}
