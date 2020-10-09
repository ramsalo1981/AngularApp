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
    public class ActiveHaulController : ControllerBase
    {
        private readonly ApplicationDb _context;

        public ActiveHaulController(ApplicationDb context)
        {
            _context = context;
        }

        // GET: api/ActiveHaul
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActiveHaul>>> GetActiveHaul()
        {
            return await _context.ActiveHaul.ToListAsync();
        }

        // GET: api/ActiveHaul/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActiveHaul>> GetActiveHaul(int id)
        {
            var activeHaul = await _context.ActiveHaul.FindAsync(id);

            if (activeHaul == null)
            {
                return NotFound();
            }

            return activeHaul;
        }

        // PUT: api/ActiveHaul/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActiveHaul(int id, ActiveHaul activeHaul)
        {
            if (id != activeHaul.ActiveHaulId)
            {
                return BadRequest();
            }

            _context.Entry(activeHaul).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActiveHaulExists(id))
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

        // POST: api/ActiveHaul
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ActiveHaul>> PostActiveHaul(ActiveHaul activeHaul)
        {
            _context.ActiveHaul.Add(activeHaul);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActiveHaul", new { id = activeHaul.ActiveHaulId }, activeHaul);
        }

        // DELETE: api/ActiveHaul/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActiveHaul>> DeleteActiveHaul(int id)
        {
            var activeHaul = await _context.ActiveHaul.FindAsync(id);
            if (activeHaul == null)
            {
                return NotFound();
            }

            _context.ActiveHaul.Remove(activeHaul);
            await _context.SaveChangesAsync();

            return activeHaul;
        }

        private bool ActiveHaulExists(int id)
        {
            return _context.ActiveHaul.Any(e => e.ActiveHaulId == id);
        }
    }
}
