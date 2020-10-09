using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using M_Rfid.Models;

namespace M_Rfid.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WagonController : ControllerBase
    {
        private readonly ApplicationDb _context;

        public WagonController(ApplicationDb context)
        {
            _context = context;
        }

        // GET: api/Wagon
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wagon>>> GetWagon()
        {
            return await _context.Wagon.ToListAsync();
        }

        // GET: api/Wagon/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wagon>> GetWagon(int id)
        {
            var wagon = await _context.Wagon.FindAsync(id);

            if (wagon == null)
            {
                return NotFound();
            }

            return wagon;
        }

        // PUT: api/Wagon/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWagon(int id, Wagon wagon)
        {
            if (id != wagon.WagonId)
            {
                return BadRequest();
            }

            _context.Entry(wagon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WagonExists(id))
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

        // POST: api/Wagon
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Wagon>> PostWagon(Wagon wagon)
        {
            _context.Wagon.Add(wagon);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWagon", new { id = wagon.WagonId }, wagon);
        }

        // DELETE: api/Wagon/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wagon>> DeleteWagon(int id)
        {
            var wagon = await _context.Wagon.FindAsync(id);
            if (wagon == null)
            {
                return NotFound();
            }

            _context.Wagon.Remove(wagon);
            await _context.SaveChangesAsync();

            return wagon;
        }

        private bool WagonExists(int id)
        {
            return _context.Wagon.Any(e => e.WagonId == id);
        }
    }
}
