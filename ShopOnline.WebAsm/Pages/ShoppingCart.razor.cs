namespace ShopOnline.WebAsm.Pages;

public partial class ShoppingCart
{
    [Inject]
    public IJSRuntime JS { get; set; } = null!;
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; } = null!;
    public List<CartItemDto>? CartItems { get; set; }
    public string? ErrorMessage { get; set; }
    public string? TotalPrice { get; set; }
    public int TotalQuantity { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            CartItems = await ShoppingCartService.GetItems(HardCoded.UserId);

            CartChanged();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    protected async Task UpdateQty_Input(int id)
    {
        await MakeUpdateQtyButtonVisible(id, true);
    }

    private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
    {
        await JS.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
    }

    protected async Task DeleteCartItem_Click(int id)
    {
        var cartItemDto = await ShoppingCartService.DeleteItem(id);

        RemoveCartItem(id);

        CartChanged();
    }

    protected async Task UpdateQtyCartItem_Click(int id, int qty)
    {
        try
        {
            if (qty > 0)
            {
                var updateItemDto = new CartItemQtyUpdateDto { CartItemId = id, Qty = qty };

                var returnedUpdateItemDto = await ShoppingCartService.UpdateQty(updateItemDto);

                if (returnedUpdateItemDto != null)
                {
                    UpdateItemTotalPrice(returnedUpdateItemDto);
                }
            }
            else
            {
                var item = CartItems?.FirstOrDefault(x => x.Id == id);

                if (item is not null)
                {
                    item.Qty = 1;
                    item.TotalPrice = item.Price;
                }
            }

            CartChanged();

            await MakeUpdateQtyButtonVisible(id, false);
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private void UpdateItemTotalPrice(CartItemDto cartItemDto)
    {
        var item = GetCartItem(cartItemDto.Id);

        if (item is not null)
        {
            item.TotalPrice = cartItemDto.Price * cartItemDto.Qty;
        }
    }

    private void CalculateCartSummaryTotals()
    {
        SetTotalPrice();
        SetTotalQuantity();
    }

    private void SetTotalPrice()
    {
        TotalPrice = CartItems?.Sum(p => p.TotalPrice).ToString();
    }

    private void SetTotalQuantity()
    {
        TotalQuantity = CartItems is null ? 0 : CartItems.Sum(q => q.Qty);
    }

    private CartItemDto? GetCartItem(int id)
    {
        return CartItems?.FirstOrDefault(i => i.Id == id);
    }

    private void RemoveCartItem(int id)
    {
        var cartItemDto = GetCartItem(id);

        if (cartItemDto is not null)
        {
            CartItems!.Remove(cartItemDto);
        }
    }

    private void CartChanged()
    {
        CalculateCartSummaryTotals();
        ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
    }
}
