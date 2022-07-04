namespace ShopOnline.WebAsm.Pages;

public partial class ProductDetails
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public IProductService ProductService { get; set; } = null!;

    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; } = null!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;

    public string? ErrorMassege { get; set; }

    public ProductDto? Product { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Product = await ProductService.GetItem(Id);
        }
        catch (Exception ex)
        {
            ErrorMassege = ex.Message;
        }
    }

    protected async Task AddToCart_Click(CartItemToAddDto cartItemToAddDto)
    {
        try
        {
            var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDto);

            NavigationManager.NavigateTo("/ShoppingCart");
        }
        catch (Exception)
        {
            // TODO Log exception
        }
    }
}
