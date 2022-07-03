namespace ShopOnline.WebAsm.Pages;

public partial class ProductDetails
{
    [Parameter]
    public int Id { get; set; }

    [Inject]
    public IProductService ProductService { get; set; }

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
}
