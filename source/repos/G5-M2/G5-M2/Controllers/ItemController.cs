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

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Item>>> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Search term required.");

            var results = await _db.Items
                .Where(i => i.Name.Contains(query) || i.Code.Contains(query) || i.Brand.Contains(query))
                .ToListAsync();

            if (results.Count == 0) return NotFound("No items found.");
            return Ok(results);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<IEnumerable<Item>>> GetPaged([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var items = await _db.Items
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync();

            return Ok(items);
        }

        [HttpGet("sort")]
        public async Task<ActionResult<IEnumerable<Item>>> Sort([FromQuery] string by = "name")
        {
            IQueryable<Item> query = _db.Items;

            query = by.ToLower() switch
            {
                "price" => query.OrderBy(i => i.UnitPrice),
                "brand" => query.OrderBy(i => i.Brand),
                _ => query.OrderBy(i => i.Name),
            };

            return Ok(await query.ToListAsync());
        }


        [HttpPost]
        public async Task<ActionResult<Item>> Create([FromBody] Item item)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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
