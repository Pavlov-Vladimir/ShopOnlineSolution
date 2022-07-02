namespace ShopOnline.WebAsm.Pages;

public class ProductsBase : ComponentBase
{
    [Inject]
    public IProductService ProductService { get; set; } = null!;

    public IEnumerable<ProductDto>? Products { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = await ProductService.GetItems();
    }

    protected IOrderedEnumerable<IGrouping<int, ProductDto>> GetGroupedProductsByCategory()
    {
        return (from product in Products
                group product by product.CategoryId into productsByCategoryGroup
                orderby productsByCategoryGroup.Key
                select productsByCategoryGroup);
    }

    protected string? GetCategoryName(IGrouping<int, ProductDto> groupedProductDtos)
    {
        return groupedProductDtos.FirstOrDefault(pg => pg.CategoryId == groupedProductDtos.Key)?.CategoryName;
    }
}
