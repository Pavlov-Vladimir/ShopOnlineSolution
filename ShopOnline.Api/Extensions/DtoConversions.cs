namespace ShopOnline.Api.Extensions;

public static class DtoConversions
{
    public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
    {
        return (from product in products                
                select new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = product.ProductCategory.Id,
                    CategoryName = product.ProductCategory.Name,
                    ImageURL = product.ImageURL,
                    Price = product.Price,
                    Qty = product.Qty
                }).ToList();
    }

    public static ProductDto ConvertToDto(this Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.ProductCategory.Id,
            CategoryName = product.ProductCategory.Name,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Qty = product.Qty
        };
    }

    public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
    {
        return (from cartItem in cartItems
                join product in products
                on cartItem.ProductId equals product.Id
                select new CartItemDto
                {
                    Id = cartItem.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductDescription = product.Description,
                    ProductImageURL = product.ImageURL,
                    Price = product.Price,
                    CartId = cartItem.CartId,
                    Qty = cartItem.Qty,
                    TotalPrice = product.Price * cartItem.Qty
                }).ToList();
    }

    public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
    {
        return new CartItemDto
        {
            Id = cartItem.Id,
            ProductId = product.Id,
            ProductName = product.Name,
            ProductDescription = product.Description,
            ProductImageURL = product.ImageURL,
            Price = product.Price,
            CartId = cartItem.CartId,
            Qty = cartItem.Qty,
            TotalPrice = product.Price * cartItem.Qty
        };
    }

    public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> categories)
    {
        return (from category in categories
                select new ProductCategoryDto
                {
                    Id = category.Id,
                    Name = category.Name,
                    IconCSS = category.IconCSS
                }).ToList();
    }
}
