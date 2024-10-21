using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationPortal.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace JobApplicationPortal.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]

    [Route("admin/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DbJobPortalContext _context;

        public TestController(DbJobPortalContext context)
        {
            _context = context;
        }

        // GET: Test
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TTest>>> GetTTests()
        {
            if (_context.TTests == null)
            {
                return NotFound();
            }
            else
            {
                var tests = await _context.TTests
                                      .Where(t => t.TStastus == true)
                                      .OrderByDescending(t => t.TUpdateDate)
                                      .ToListAsync();
                return Ok(tests);
            }
        }

        // GET: api/Test/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TTest>> GetTTest(int id)
        {
            if (_context.TTests == null)
            {
                return NotFound();
            }
            var tTest = await _context.TTests.FindAsync(id);

            if (tTest == null)
            {
                return NotFound();
            }

            return tTest;
        }

        // PUT: api/Test/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTTest(int id, TTest tTest)
        {
            if (id != tTest.TId)
            {
                return BadRequest();
            }

            tTest.TUpdateDate = DateTime.UtcNow;
            _context.Entry(tTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TTestExists(id))
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

        // POST: api/Test
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TTest>> PostTTest([FromBody] TTest tTest)
        {
            if (_context.TTests == null)
            {
                return Problem("Entity set 'DbJobPortalContext.TTests'  is null.");
            }
            _context.TTests.Add(tTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTTest", new { id = tTest.TId }, tTest);
        }

        // DELETE: Test/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTTest(int id)
        {
            if (_context.TTests == null)
            {
                return NotFound();
            }

            try
            {
                var tTest = _context.TTests
                    .FirstOrDefault(t => t.TId == id);

                if (tTest == null)
                {
                    return NotFound();
                }
                else
                {
                    tTest.TStastus = false;
                    tTest.TUpdateDate = DateTime.UtcNow;
                    await _context.SaveChangesAsync();
                    return NoContent();
                }

            }
            catch (Exception ex)
            {
                return StatusCode(5, ex.Message.ToString());
            }
           
        }

        private bool TTestExists(int id)
        {
            return (_context.TTests?.Any(e => e.TId == id)).GetValueOrDefault();
        }
    }
}
