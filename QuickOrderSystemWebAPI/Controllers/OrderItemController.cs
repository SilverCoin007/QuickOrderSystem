using Microsoft.AspNetCore.Mvc;
using QuickOrderSystemClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickOrderSystemWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public OrderItemController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("byorder/{orderId}")]
        public ActionResult<IEnumerable<OrderItem>> GetAllByOrderId(int orderId)
        {
            return _context.OrderItems.Where(oi => oi.OrderId == orderId).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<OrderItem> GetById(int id)
        {
            var orderItem = _context.OrderItems.Find(id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return orderItem;
        }

        [HttpPost]
        public ActionResult Create(OrderItem orderItem)
        {
            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = orderItem.Id }, orderItem);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            var existingOrderItem = _context.OrderItems.Find(id);
            if (existingOrderItem == null)
            {
                return NotFound();
            }

            existingOrderItem.ProductId = orderItem.ProductId;
            existingOrderItem.Quantity = orderItem.Quantity;

            _context.OrderItems.Update(existingOrderItem);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var orderItem = _context.OrderItems.Find(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
