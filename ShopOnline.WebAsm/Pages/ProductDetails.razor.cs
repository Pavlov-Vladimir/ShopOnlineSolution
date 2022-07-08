namespace ShopOnline.WebAsm.Pages;

public partial class ProductDetails
{
    private List<CartItemDto>? CartItems { get; set; }

    [Parameter]
    public int Id { get; set; }
    [Inject]
    public IProductService ProductService { get; set; } = null!;
    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; } = null!;
    [Inject]
    public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; } = null!;
    [Inject]
    public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; } = null!;
    [Inject]
    public NavigationManager NavigationManager { get; set; } = null!;
    public string? ErrorMassege { get; set; }
    public ProductDto? Product { get; set; }


    protected override async Task OnInitializedAsync()
    {
        try
        {
            CartItems = await ManageCartItemsLocalStorageService.GetCollection();
            Product = await GetProductById(Id);
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

            if (cartItemDto is not null && CartItems is not null)
            {
                CartItems.Add(cartItemDto);
                await ManageCartItemsLocalStorageService.SaveCollection(CartItems);
            }

            NavigationManager.NavigateTo("/ShoppingCart");
        }
        catch (Exception)
        {
            // TODO Log exception
        }
    }

    private async Task<ProductDto?> GetProductById(int id)
    {
        var productDtos = await ManageProductsLocalStorageService.GetCollection();

        if (productDtos is not null)
        {
            return productDtos.SingleOrDefault(p => p.Id == id);
        }
        return null;
    }
}
