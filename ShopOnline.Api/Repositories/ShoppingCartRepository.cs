namespace ShopOnline.Api.Repositories;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly ShopOnlineDbContext _shopOnlineDbContext;

    public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
    {
        _shopOnlineDbContext = shopOnlineDbContext;
    }

    private async Task<bool> CartItemExists(int cartId, int productId)
    {
        return await _shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
    }

    public async Task<CartItem?> AddItem(CartItemToAddDto cartItemToAddDto)
    {
        if (await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId) == false)
        {
            var item = await (from product in _shopOnlineDbContext.Products
                              where product.Id == cartItemToAddDto.ProductId
                              select new CartItem
                              {
                                  CartId = cartItemToAddDto.CartId,
                                  ProductId = product.Id,
                                  Qty = cartItemToAddDto.Qty,
                              }).SingleOrDefaultAsync();

            if (item is not null)
            {
                var result = await _shopOnlineDbContext.CartItems.AddAsync(item);
                await _shopOnlineDbContext.SaveChangesAsync();
                return result.Entity;
            } 
        }

        return null;
    }

    public Task<CartItem> DeleteItem(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CartItem?> GetItem(int id)
    {
        return await (from cart in _shopOnlineDbContext.Carts
                      join cartItem in _shopOnlineDbContext.CartItems
                      on cart.Id equals cartItem.CartId
                      where cartItem.Id == id
                      select new CartItem
                      {
                          Id = cartItem.Id,
                          CartId = cartItem.CartId,
                          ProductId = cartItem.ProductId,
                          Qty = cartItem.Qty
                      }).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<CartItem>> GetItems(int userId)
    {
        return await (from cart in _shopOnlineDbContext.Carts
                      join cartItem in _shopOnlineDbContext.CartItems
                      on cart.Id equals cartItem.CartId
                      where cart.UserId == userId
                      select new CartItem
                      {
                          Id = cartItem.Id,
                          CartId = cartItem.CartId,
                          Qty = cartItem.Qty,
                          ProductId = cartItem.ProductId
                      }).ToListAsync();
    }

    public Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
    {
        throw new NotImplementedException();
    }
}
