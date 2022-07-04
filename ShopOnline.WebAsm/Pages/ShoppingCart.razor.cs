namespace ShopOnline.WebAsm.Pages;

public partial class ShoppingCart
{
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; } = null!;

    public List<CartItemDto>? CartItems { get; set; }

    public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            CartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected async Task DeleteCartItem_Click(int id)
    {
        var cartItemDto = await ShoppingCartService.DeleteItem(id);

        RemoveCartItem(id);

    }

    private CartItemDto? GetCartItemDto(int id)
    {
        return CartItems?.FirstOrDefault(i => i.Id == id);
    }

    private void RemoveCartItem(int id)
    {
        var cartItemDto = GetCartItemDto(id);

        if (cartItemDto is not null)
        {
            CartItems?.Remove(cartItemDto);
        }
    }
}
