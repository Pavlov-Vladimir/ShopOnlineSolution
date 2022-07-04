namespace ShopOnline.WebAsm.Pages;

public partial class ShoppingCart
{
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; } = null!;

    public IEnumerable<CartItemDto>? CartItems { get; set; }

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
}
