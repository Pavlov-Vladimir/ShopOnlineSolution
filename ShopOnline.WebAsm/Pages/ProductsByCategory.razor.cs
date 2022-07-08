namespace ShopOnline.WebAsm.Pages;

public partial class ProductsByCategory
{
    [Parameter]
    public int CategoryId { get; set; }
    [Inject]
    public IProductService ProductService { get; set; } = null!;
    public string? ErrorMessage { get; set; }
    public string? CategoryName { get; set; }
    public IEnumerable<ProductDto>? Products { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            Products = await ProductService.GetProductsByCategory(CategoryId);
            if (Products is not null && Products.Any())
            {
                var productDto = Products.FirstOrDefault(p => p.CategoryId == CategoryId);
                if (productDto is not null)
                {
                    CategoryName = productDto.CategoryName;
                }
            }            
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
