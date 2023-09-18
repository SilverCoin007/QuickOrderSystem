using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickOrderSystemWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using QuickOrderSystemClassLibrary;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QrCode>>> GetQrCodes()
        {
            if (_dbContext.Products == null)
            {
                return NotFound();
            }
            return await _dbContext.QrCodes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QrCode>> GetQrCodes(int id)
        {
            if (_dbContext.QrCodes == null)
            {
                return NotFound();
            }
            var qrCode = await _dbContext.QrCodes.FindAsync(id);
            if (qrCode == null)
            {
                return NotFound();
            }
            return qrCode;
        }

        [HttpPost]
        public async Task<ActionResult<QrCode>> PostQrCode(QrCode qrCode)
        {
            _dbContext.QrCodes.Add(qrCode);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQrCodes), new { id = qrCode.ID }, qrCode);
        }

        [HttpPut]
        public async Task<ActionResult> PutQrCode(int id, QrCode qrCode)
        {
            if (id != qrCode.ID)
            {
                return BadRequest();
            }
            _dbContext.Entry(qrCode).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandAvailable(id))
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

        private bool BrandAvailable(int id)
        {
            return (_dbContext.QrCodes?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteQrCode(int id)
        {
            if (_dbContext.QrCodes == null)
            {
                return NotFound();
            }

            var qrCode = await _dbContext.QrCodes.FindAsync(id);
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
