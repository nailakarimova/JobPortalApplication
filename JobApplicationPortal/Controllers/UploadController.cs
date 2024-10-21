using JobApplicationPortal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;


namespace JobApplicationPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly DbJobPortalContext _context;

        private readonly string _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedResumes");

        public UploadController(DbJobPortalContext context)
        {
            if (!Directory.Exists(_uploadFolderPath))
            {
                Directory.CreateDirectory(_uploadFolderPath);
            }
            _context = context;
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadCV(IFormFile resume, int candidateId)
        {
            if (resume == null || resume.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var fileExtension = Path.GetExtension(resume.FileName);
            if (fileExtension != ".pdf" && fileExtension != ".docx")
            {
                return BadRequest("Only PDF or DOCX files are allowed.");
            }

            if (resume.Length > 5 * 1024 * 1024) // 5MB
            {
                return BadRequest("File size must be less than 5MB.");
            }

            var uniqueFileName = Guid.NewGuid() + fileExtension;
            var filePath = Path.Combine(_uploadFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await resume.CopyToAsync(stream);
            }

            await savePathToDatabase(candidateId, uniqueFileName);

            return Ok(new { FilePath = uniqueFileName });
        }

        private async Task savePathToDatabase(int candidateId, string resumePath)
        {
            var candidate = await _context.TCandidates.FindAsync(candidateId);
            if (candidate != null)
            {
                candidate.CResumePath = resumePath; 
                await _context.SaveChangesAsync(); 
            }
            else
            {
                throw new Exception("Candidate not found.");
            }
        }
    }
}