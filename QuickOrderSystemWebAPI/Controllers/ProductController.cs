using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickOrderSystemClassLibrary;
using System.Linq;
using System.Security.Claims;

namespace QuickOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public ProductController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private int GetCurrentUserIdFromHeader()
        {
            var userIdHeader = Request.Headers["UserId"].FirstOrDefault();
            return int.TryParse(userIdHeader, out var userId) ? userId : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return await _dbContext.Products.Where(p => p.UserId == currentUserId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var product = await _dbContext.Products.Where(p => p.UserId == currentUserId && p.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            product.UserId = currentUserId;

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            var currentUserId = GetCurrentUserIdFromHeader();
            if (product.UserId != currentUserId)
            {
                return Unauthorized();
            }

            _dbContext.Entry(product).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }

        private bool ProductAvailable(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return _dbContext.Products.Any(p => p.Id == id && p.UserId == currentUserId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var product = await _dbContext.Products.Where(p => p.UserId == currentUserId && p.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
