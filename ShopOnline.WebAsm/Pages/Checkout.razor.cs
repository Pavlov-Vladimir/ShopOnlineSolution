namespace ShopOnline.WebAsm.Pages;

public partial class Checkout
{
    [Inject]
    public IJSRuntime JS { get; set; } = null!;
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; } = null!;
    [Inject]
    public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; } = null!;
    protected IEnumerable<CartItemDto>? CartItems { get; set; }
    protected int TotalQty { get; set; }
    protected string? PaymentDescription { get; set; }
    protected decimal PaymentAmount { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            CartItems = await ManageCartItemsLocalStorageService.GetCollection();

            if (CartItems is not null)
            {
                Guid orderGuid = Guid.NewGuid();

                PaymentAmount = CartItems.Sum(x => x.TotalPrice);
                TotalQty = CartItems.Sum(x => x.Qty);
                PaymentDescription = $"O_{HardCoded.UserId}_{orderGuid}";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("initPayPalButton");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}
