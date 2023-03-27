using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using foodorder.Models;

namespace foodorder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class foodItemsController : ControllerBase
    {
        private readonly foodorderContext _context;

        public foodItemsController(foodorderContext context)
        {
            _context = context;
        }

        // GET: api/foodItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<foodItem>>> GetFoodItems()
        {
          if (_context.FoodItems == null)
          {
              return NotFound();
          }
            return await _context.FoodItems.ToListAsync();
        }

        // GET: api/foodItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<foodItem>> GetfoodItem(int id)
        {
          if (_context.FoodItems == null)
          {
              return NotFound();
          }
            var foodItem = await _context.FoodItems.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            return foodItem;
        }

        // PUT: api/foodItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutfoodItem(int id, foodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(foodItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!foodItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/foodItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<foodItem>> PostfoodItem(foodItem foodItem)
        {
          if (_context.FoodItems == null)
          {
              return Problem("Entity set 'foodorderContext.FoodItems'  is null.");
          }
            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetfoodItem), new { id = foodItem.Id }, foodItem);
        }

        // DELETE: api/foodItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletefoodItem(int id)
        {
            if (_context.FoodItems == null)
            {
                return NotFound();
            }
            var foodItem = await _context.FoodItems.FindAsync(id);
            if (foodItem == null)
            {
                return NotFound();
            }

            _context.FoodItems.Remove(foodItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool foodItemExists(int id)
        {
            return (_context.FoodItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
