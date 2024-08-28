using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BE.Entities;

namespace BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DM_THELOAIController : ControllerBase
    {
        private readonly EShoppingContext _context;

        public DM_THELOAIController(EShoppingContext context)
        {
            _context = context;
        }

        // GET: api/DM_THELOAI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DM_THELOAI>>> GetDM_THELOAIs()
        {
            return await _context.DM_THELOAIs.ToListAsync();
        }

        // GET: api/DM_THELOAI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DM_THELOAI>> GetDM_THELOAI(Guid id)
        {
            var dM_THELOAI = await _context.DM_THELOAIs.FindAsync(id);

            if (dM_THELOAI == null)
            {
                return NotFound();
            }

            return dM_THELOAI;
        }

        // PUT: api/DM_THELOAI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDM_THELOAI(Guid id, DM_THELOAI dM_THELOAI)
        {
            if (id != dM_THELOAI.Id)
            {
                return BadRequest();
            }

            _context.Entry(dM_THELOAI).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DM_THELOAIExists(id))
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

        // POST: api/DM_THELOAI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DM_THELOAI>> PostDM_THELOAI(DM_THELOAI dM_THELOAI)
        {
            _context.DM_THELOAIs.Add(dM_THELOAI);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DM_THELOAIExists(dM_THELOAI.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDM_THELOAI", new { id = dM_THELOAI.Id }, dM_THELOAI);
        }

        // DELETE: api/DM_THELOAI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDM_THELOAI(Guid id)
        {
            var dM_THELOAI = await _context.DM_THELOAIs.FindAsync(id);
            if (dM_THELOAI == null)
            {
                return NotFound();
            }

            _context.DM_THELOAIs.Remove(dM_THELOAI);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DM_THELOAIExists(Guid id)
        {
            return _context.DM_THELOAIs.Any(e => e.Id == id);
        }
    }
}
