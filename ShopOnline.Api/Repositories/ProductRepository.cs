namespace ShopOnline.Api.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ShopOnlineDbContext _shopOnlineDbContext;

    public ProductRepository(ShopOnlineDbContext shopOnlineDbContext)
    {
        _shopOnlineDbContext = shopOnlineDbContext;
    }

    public async Task<IEnumerable<ProductCategory>> GetCategories()
    {
        var categories = await _shopOnlineDbContext.ProductCategories.ToListAsync();
        return categories;
    }

    public async Task<ProductCategory?> GetCategory(int id)
    {
        var category = await _shopOnlineDbContext.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
        return category;
    }

    public async Task<Product?> GetItem(int id)
    {
        var product = await _shopOnlineDbContext.Products
            .Include(p => p.ProductCategory)
            .SingleOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public async Task<IEnumerable<Product>> GetItems()
    {
        var products = await _shopOnlineDbContext.Products
            .Include(p => p.ProductCategory)
            .ToListAsync();
        return products;
    }

    public async Task<IEnumerable<Product>> GetItemsByCategory(int categoryId)
    {
        var products = await _shopOnlineDbContext.Products
            .Include(p => p.ProductCategory)
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
        return products;
    }
}
