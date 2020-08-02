using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using wbapi.Models;

namespace wbapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class wbController : ControllerBase
    {
        private readonly wbContext _context;

        public wbController(wbContext context)
        {
            _context = context;
        }

        // GET: api/wb
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wb>>> GetWb()
        {
            return await _context.Wb.ToListAsync();
        }

        // GET: api/wb/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wb>> GetWb(string id)
        {
            var wb = await _context.Wb.FindAsync(id);

            if (wb == null)
            {
                return NotFound();
            }

            return wb;
        }

        // PUT: api/wb/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWb(string id, Wb wb)
        {
            if (id != wb.Seq)
            {
                return BadRequest();
            }

            _context.Entry(wb).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WbExists(id))
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

        // POST: api/wb
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Wb>> PostWb(Wb wb)
        {
            _context.Wb.Add(wb);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WbExists(wb.Seq))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWb", new { id = wb.Seq }, wb);
        }

        // DELETE: api/wb/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Wb>> DeleteWb(string id)
        {
            var wb = await _context.Wb.FindAsync(id);
            if (wb == null)
            {
                return NotFound();
            }

            _context.Wb.Remove(wb);
            await _context.SaveChangesAsync();

            return wb;
        }

        private bool WbExists(string id)
        {
            return _context.Wb.Any(e => e.Seq == id);
        }
    }
}
