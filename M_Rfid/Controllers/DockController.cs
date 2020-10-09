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
    public class DockController : ControllerBase
    {
        private readonly ApplicationDb _context;

        public DockController(ApplicationDb context)
        {
            _context = context;
        }

        // GET: api/Dock
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dock>>> GetDock()
        {
            return await _context.Dock.ToListAsync();
        }

        // GET: api/Dock/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dock>> GetDock(int id)
        {
            var dock = await _context.Dock.FindAsync(id);

            if (dock == null)
            {
                return NotFound();
            }

            return dock;
        }

        // PUT: api/Dock/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDock(int id, Dock dock)
        {
            if (id != dock.DockId)
            {
                return BadRequest();
            }

            _context.Entry(dock).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DockExists(id))
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

        // POST: api/Dock
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Dock>> PostDock(Dock dock)
        {
            _context.Dock.Add(dock);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDock", new { id = dock.DockId }, dock);
        }

        // DELETE: api/Dock/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dock>> DeleteDock(int id)
        {
            var dock = await _context.Dock.FindAsync(id);
            if (dock == null)
            {
                return NotFound();
            }

            _context.Dock.Remove(dock);
            await _context.SaveChangesAsync();

            return dock;
        }

        private bool DockExists(int id)
        {
            return _context.Dock.Any(e => e.DockId == id);
        }
    }
}
