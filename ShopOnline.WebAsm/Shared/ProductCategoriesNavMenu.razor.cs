namespace ShopOnline.WebAsm.Shared;

public partial class ProductCategoriesNavMenu
{
    [Inject]
    public IProductService ProductService { get; set; } = null!;
    public IEnumerable<ProductCategoryDto>? ProductCategoryDtos { get; set; }
    public string? ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            ProductCategoryDtos = await ProductService.GetProductCategories();
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
