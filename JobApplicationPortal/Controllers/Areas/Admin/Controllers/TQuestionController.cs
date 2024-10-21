using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationPortal.Models;

namespace JobApplicationPortal.Controllers.Areas.Admin.Controllers
{
    [Area("Admin")]
    [ApiController]

    [Route("admin/[controller]")]
    public class TQuestionController : ControllerBase
    {
        private readonly DbJobPortalContext _context;

        public TQuestionController(DbJobPortalContext context)
        {
            _context = context;
        }

        // GET: api/TQuestion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TQuestion>>> GetTQuestions()
        {
            if (_context.TQuestions == null)
            {
                return NotFound();
            }
            return await _context.TQuestions
                .Where(q => q.QStatus == true) 
                .ToListAsync();
        }

        // GET: api/TQuestion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TQuestion>> GetTQuestion(int id)
        {
            var question = await _context.TQuestions
                .FirstOrDefaultAsync(q => q.QId == id && q.QStatus == true); 

            if (question == null)
            {
                return NotFound();
            }

            return question;
        }

        // PUT: api/TQuestion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTQuestion(int id, TQuestion tQuestion)
        {
            if (id != tQuestion.QId)
            {
                return BadRequest();
            }

            tQuestion.QUpdateDate = DateTime.UtcNow;
            _context.Entry(tQuestion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TQuestionExists(id))
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

        // POST: api/TQuestion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TQuestion>> PostTQuestion(TQuestion tQuestion)
        {
            if (_context.TQuestions == null)
            {
                return Problem("Entity set 'DbJobPortalContext.TQuestions'  is null.");
            }
            _context.TQuestions.Add(tQuestion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTQuestion", new { id = tQuestion.QId }, tQuestion);
        }

        // DELETE: api/TQuestion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTQuestion(int id)
        {
            var question = await _context.TQuestions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            question.QStatus = false;
            question.QUpdateDate = DateTime.UtcNow;
            _context.Entry(question).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TQuestionExists(int id)
        {
            return (_context.TQuestions?.Any(e => e.QId == id)).GetValueOrDefault();
        }
    }
}
