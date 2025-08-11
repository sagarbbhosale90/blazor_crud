using BlazorOAuthAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorOAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IDbContextFactory<ProductDbContext> _DbFactory { get; set; }
        private ProductDbContext _DbContext;

        public ProductController(IDbContextFactory<ProductDbContext> DbFactory)
        {
            _DbFactory = DbFactory;
            _DbContext = _DbFactory.CreateDbContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> Get()
        {
            try
            {
                var products = await _DbContext.Products.ToListAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> Get(int id)
        {
            try
            {
                var product = await _DbContext.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
