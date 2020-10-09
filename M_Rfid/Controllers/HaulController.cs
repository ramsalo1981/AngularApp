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
    public class HaulController : ControllerBase
    {
        private readonly ApplicationDb _context;

        public HaulController(ApplicationDb context)
        {
            _context = context;
        }

        // GET: api/Haul
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Haul>>> GetHaul()
        {
            //var result = (from a in _context.Haul
            //              join b in _context.Dock on a.DockId equals b.DockId

            //              select new
            //              {
            //                  a.HaulId,
            //                  b.Name,
            //                  a.StartTime,
            //                  a.EndTime,
            //                  a.HaulSituation
            //              });

            //return  result.ToLis();
            return await _context.Haul.ToListAsync();

        }

        // GET: api/Haul/5
        [HttpGet("{id}")]
        public ActionResult<Haul> GetHaul(int id)
        {
            //var haul = await _context.Haul.FindAsync(id);

            //if (haul == null)
            //{
            //    return NotFound();
            //}

            //return haul;
            var haul = (from a in _context.Haul
                        where a.HaulId == id

                        select new
                        {
                            a.HaulId,

                            a.DockId,
                            a.StartTime,
                            a.EndTime,
                            a.HaulSituation,
                            DeletedActiveHaulIDs=""
                        }).FirstOrDefault();

            var haulDetails = (from a in _context.ActiveHaul
                               join b in _context.Wagon on a.WagonId equals b.WagonId
                               where a.HaulId == id

                               select new
                               {
                                   a.HaulId,
                                   a.ActiveHaulId,
                                   a.WagonId,
                                   wagonName = b.Name,
                                   a.ActiveHaulStatus,

                               }).ToList();

            return Ok(new { haul, haulDetails });
        }

        // PUT: api/Haul/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHaul(int id, Haul haul)
        {
            if (id != haul.HaulId)
            {
                return BadRequest();
            }

            _context.Entry(haul).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HaulExists(id))
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

        // POST: api/Haul
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Haul>> PostHaul(Haul haul)
        {
            try
            {
                //Haul Table
                if(haul.HaulId ==0)
                {
                    _context.Haul.Add(haul);
                }
                else
                {
                    _context.Entry(haul).State = EntityState.Modified;
                }
                
                //ActiveHaul Table
                foreach (var item in haul.ActiveHaul)
                {
                    if (item.ActiveHaulId == 0)
                    {
                        _context.ActiveHaul.Add(item);
                    }
                    else
                    {
                        _context.Entry(item).State = EntityState.Modified;
                    }

                }
                //Delete for ActiveHaul
                foreach (var id in haul.DeletedActiveHaulIDs.Split(',').Where(x => x !=""))
                {
                    ActiveHaul x = _context.ActiveHaul.Find(Convert.ToInt32(id));
                    _context.ActiveHaul.Remove(x);
                }
                await _context.SaveChangesAsync();

                return Ok();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        // DELETE: api/Haul/5
        [HttpDelete("{id}")]
        public ActionResult<Haul> DeleteHaul(int id)
        {
            var haul = _context.Haul.Include(y => y.ActiveHaul)
                .SingleOrDefault(x => x.HaulId == id);
            foreach (var item in haul.ActiveHaul.ToList())
            {
                _context.ActiveHaul.Remove(item);
            }

            _context.Haul.Remove(haul);
            _context.SaveChangesAsync();

            return haul;
        }

        private bool HaulExists(int id)
        {
            return _context.Haul.Any(e => e.HaulId == id);
        }
    }
}
