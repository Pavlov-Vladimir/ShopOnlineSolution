namespace ShopOnline.WebAsm.PageComponents;

public partial class DisplayProducts
{
    [Parameter]
    public IEnumerable<ProductDto> Products { get; set; } = null!;
}
