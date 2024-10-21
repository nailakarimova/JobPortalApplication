using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationPortal.Models;

namespace JobApplicationPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly DbJobPortalContext _context;

        public JobController(DbJobPortalContext context)
        {
            _context = context;
        }

        // GET: api/Job
        //[HttpGet]
        //public IEnumerable<Job> Get()
        //{
        //    return _context.Jobs.ToList();
        //}

        // GET: api/Job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TJob>>> GetJobs()
        {
            return await _context.TJobs.Where(j=>j.JStatus == true).ToListAsync();
        }

        // GET: api/Job/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TJob>> GetJobs(int id)
        {
            var job = await _context.TJobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // POST: api/Job
        [HttpPost]
        public async Task<ActionResult<TJob>> PostJob(TJob job)
        {
            _context.TJobs.Add(job);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetJobs), new { id = job.JId }, job);
        }

        // PUT: api/Job/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, TJob job)
        {
            if (id != job.JId)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Job/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.TJobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.TJobs.Remove(job);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}