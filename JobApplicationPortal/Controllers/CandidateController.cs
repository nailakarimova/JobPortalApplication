using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationPortal.Models;

namespace JobApplicationPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly DbJobPortalContext _context;

        public CandidateController(DbJobPortalContext context)
        {
            _context = context;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TCandidate>>> GetTCandidates()
        {
          if (_context.TCandidates == null)
          {
              return NotFound();
          }
            return await _context.TCandidates.ToListAsync();
        }

        // GET: api/Candidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TCandidate>> GetTCandidate(int id)
        {
          if (_context.TCandidates == null)
          {
              return NotFound();
          }
            var tCandidate = await _context.TCandidates.FindAsync(id);

            if (tCandidate == null)
            {
                return NotFound();
            }

            return tCandidate;
        }

        // PUT: api/Candidate/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTCandidate(int id, TCandidate tCandidate)
        {
            if (id != tCandidate.CId)
            {
                return BadRequest();
            }

            _context.Entry(tCandidate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TCandidateExists(id))
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

        // POST: api/Candidate
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TCandidate>> PostTCandidate(TCandidate tCandidate)
        {
          if (_context.TCandidates == null)
          {
              return Problem("Entity set 'DbJobPortalContext.TCandidates'  is null.");
          }
            _context.TCandidates.Add(tCandidate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTCandidate", new { id = tCandidate.CId }, tCandidate);
        }
               
        private bool TCandidateExists(int id)
        {
            return (_context.TCandidates?.Any(e => e.CId == id)).GetValueOrDefault();
        }
    }
}
