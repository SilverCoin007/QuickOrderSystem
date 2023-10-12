using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickOrderSystemClassLibrary;
using System.Linq;
using System.Security.Claims;

namespace QuickOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public CategoryController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private int GetCurrentUserIdFromHeader()
        {
            var userIdHeader = Request.Headers["UserId"].FirstOrDefault();
            return int.TryParse(userIdHeader, out var userId) ? userId : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return await _dbContext.Category.Where(c => c.UserId == currentUserId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategories(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var category = await _dbContext.Category.Where(c => c.UserId == currentUserId && c.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }
            return category;
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            category.UserId = currentUserId;

            _dbContext.Category.Add(category);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategories), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            var currentUserId = GetCurrentUserIdFromHeader();
            if (category.UserId != currentUserId)
            {
                return Unauthorized();
            }

            _dbContext.Entry(category).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryAvailable(id))
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

        private bool CategoryAvailable(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return (_dbContext.Category.Any(x => x.Id == id && x.UserId == currentUserId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var category = await _dbContext.Category.Where(c => c.UserId == currentUserId && c.Id == id).FirstOrDefaultAsync();

            if (category == null)
            {
                return NotFound();
            }

            _dbContext.Category.Remove(category);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
