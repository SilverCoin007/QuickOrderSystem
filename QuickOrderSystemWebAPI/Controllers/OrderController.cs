using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickOrderSystemClassLibrary;
using System.Linq;
using System.Security.Claims;

namespace QuickOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public OrderController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private int GetCurrentUserIdFromHeader()
        {
            var userIdHeader = Request.Headers["UserId"].FirstOrDefault();
            return int.TryParse(userIdHeader, out var userId) ? userId : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return await _dbContext.Orders.Where(o => o.UserId == currentUserId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrders(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var order = await _dbContext.Orders.Where(o => o.UserId == currentUserId && o.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }
            return order;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            order.UserId = currentUserId;

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            var currentUserId = GetCurrentUserIdFromHeader();
            if (order.UserId != currentUserId)
            {
                return Unauthorized();
            }

            _dbContext.Entry(order).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderAvailable(id))
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

        private bool OrderAvailable(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return (_dbContext.Orders.Any(x => x.Id == id && x.UserId == currentUserId));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var order = await _dbContext.Orders.Where(o => o.UserId == currentUserId && o.Id == id).FirstOrDefaultAsync();

            if (order == null)
            {
                return NotFound();
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
