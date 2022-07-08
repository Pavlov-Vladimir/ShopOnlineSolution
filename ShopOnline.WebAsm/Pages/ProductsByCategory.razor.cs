namespace ShopOnline.WebAsm.Pages;

public partial class ProductsByCategory
{
    [Parameter]
    public int CategoryId { get; set; }
    [Inject]
    public IProductService ProductService { get; set; } = null!;
    [Inject]
    public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; } = null!;
    public string? ErrorMessage { get; set; }
    public string? CategoryName { get; set; }
    public IEnumerable<ProductDto>? Products { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Products = await GetProductCollectionByCategoryId(CategoryId);

            if (Products is not null && Products.Any())
            {
                var productDto = Products.First();
                CategoryName = productDto.CategoryName;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }

    private async Task<IEnumerable<ProductDto>?> GetProductCollectionByCategoryId(int categoryId)
    {
        var products = await ManageProductsLocalStorageService.GetCollection();

        if (products is not null)
        {
            return products.Where(x => x.CategoryId == categoryId);
        }
        else
        {
            return await ProductService.GetProductsByCategory(categoryId);
        }
    }
}
