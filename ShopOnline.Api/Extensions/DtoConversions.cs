using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Extensions;

public static class DtoConversions
{
    public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products,
                                                IEnumerable<ProductCategory> categories)
    {
        return (from product in products
                join category in categories
                on product.CategoryId equals category.Id
                select new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    ImageURL = product.ImageURL,
                    Price = product.Price,
                    Qty = product.Qty
                }).ToList();
    }

    public static ProductDto ConvertToDto(this Product product, ProductCategory category)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = category.Id,
            CategoryName = category.Name,
            ImageURL = product.ImageURL,
            Price = product.Price,
            Qty = product.Qty
        };
    }
}
