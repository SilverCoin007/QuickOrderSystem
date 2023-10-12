using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuickOrderSystemClassLibrary;
using System.Linq;
using System.Security.Claims;

namespace QuickOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public QrCodeController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private int GetCurrentUserIdFromHeader()
        {
            var userIdHeader = Request.Headers["UserId"].FirstOrDefault();
            return int.TryParse(userIdHeader, out var userId) ? userId : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QrCode>>> GetQrCodes()
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return await _dbContext.QrCodes.Where(q => q.UserId == currentUserId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QrCode>> GetQrCodes(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var qrCode = await _dbContext.QrCodes.Where(q => q.UserId == currentUserId && q.Id == id).FirstOrDefaultAsync();

            if (qrCode == null)
            {
                return NotFound();
            }
            return qrCode;
        }

        [HttpPost]
        public async Task<ActionResult<QrCode>> PostQrCode(QrCode qrCode)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            qrCode.UserId = currentUserId;

            _dbContext.QrCodes.Add(qrCode);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQrCodes), new { id = qrCode.Id }, qrCode);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutQrCode(int id, QrCode qrCode)
        {
            if (id != qrCode.Id)
            {
                return BadRequest();
            }

            var currentUserId = GetCurrentUserIdFromHeader();
            if (qrCode.UserId != currentUserId)
            {
                return Unauthorized();
            }

            _dbContext.Entry(qrCode).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QrCodeAvailable(id))
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

        private bool QrCodeAvailable(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            return _dbContext.QrCodes.Any(q => q.Id == id && q.UserId == currentUserId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQrCode(int id)
        {
            var currentUserId = GetCurrentUserIdFromHeader();
            var qrCode = await _dbContext.QrCodes.Where(q => q.UserId == currentUserId && q.Id == id).FirstOrDefaultAsync();

            if (qrCode == null)
            {
                return NotFound();
            }

            _dbContext.QrCodes.Remove(qrCode);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
