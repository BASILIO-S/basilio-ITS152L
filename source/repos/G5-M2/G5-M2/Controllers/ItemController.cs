using G5M2.Data;
using G5M2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G5M2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ItemsController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAll()
        {
            return await _db.Items.AsNoTracking().ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Item>> Get(int id)
        {
            var item = await _db.Items.FindAsync(id);
            if (item == null) return NotFound();
            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Item>> Create([FromBody] Item item)
        {
            _db.Items.Add(item);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Item updated)
        {
            if (id != updated.Id) return BadRequest();
            var exist = await _db.Items.FindAsync(id);
            if (exist == null) return NotFound();

            exist.Name = updated.Name;
            exist.Code = updated.Code;
            exist.Brand = updated.Brand;
            exist.UnitPrice = updated.UnitPrice;

            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var exist = await _db.Items.FindAsync(id);
            if (exist == null) return NotFound();
            _db.Items.Remove(exist);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
