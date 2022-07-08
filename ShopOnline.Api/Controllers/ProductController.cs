namespace ShopOnline.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
    {
        try
        {
            var products = await _productRepository.GetItems();

            if (products is null)
            {
                return NotFound();
            }
            else
            {
                var productDtos = products.ConvertToDto();
                return Ok(productDtos);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              "Error retrieving data from the database.");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetItem(int id)
    {
        try
        {
            var product = await _productRepository.GetItem(id);

            if (product is null)
            {
                return BadRequest();
            }
            else
            {
                var productDto = product.ConvertToDto();
                return Ok(productDto);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              "Error retrieving data from the database.");
        }
    }

    [HttpGet]
    [Route(nameof(GetProductCategories))]
    public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
    {
        try
        {
            var categories = await _productRepository.GetCategories();
            var categoryDtos = categories.ConvertToDto();

            return Ok(categoryDtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              "Error retrieving data from the database.");
        }
    }

    [HttpGet]
    [Route("{categoryId:int}/GetItemsByCategory")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
    {
        try
        {
            var products = await _productRepository.GetItemsByCategory(categoryId);
            var productDtos = products.ConvertToDto();

            return Ok(productDtos);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                              "Error retrieving data from the database.");
        }
    }
}
