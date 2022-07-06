namespace ShopOnline.WebAsm.Pages;

public class ProductsBase : ComponentBase
{
    [Inject]
    public IProductService ProductService { get; set; } = null!;

    [Inject]
    public IShoppingCartService ShoppingCartService { get; set; } = null!;

    public IEnumerable<ProductDto>? Products { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Products = await ProductService.GetItems();

        var shoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
        var totalQty = shoppingCartItems is null ? 0 : shoppingCartItems.Sum(i => i.Qty);

        ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
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
