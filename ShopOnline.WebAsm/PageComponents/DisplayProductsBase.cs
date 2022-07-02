namespace ShopOnline.WebAsm.PageComponents;

public class DisplayProductsBase : ComponentBase
{
    [Parameter]
    public IEnumerable<ProductDto>? Products { get; set; }
}
