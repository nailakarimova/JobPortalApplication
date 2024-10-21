using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplicationPortal.Models;
using System.Diagnostics;
using JobApplicationPortal.Helpers;
using Humanizer;
using NuGet.DependencyResolver;

namespace JobApplicationPortal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly DbJobPortalContext _context;

        public QuizController(DbJobPortalContext context)
        {
            _context = context;
        }

        // GET: Quiz
        [HttpGet("{testId}")]
        public async Task<ActionResult<List<QuestionWithAnswers>>> GetTQuestions(int testId)
        {
            if (_context.TQuestions == null)
            {
                return NotFound();
            }

                var dbResult = _context.TQuestions
                    .Where(q => q.QTId == testId && q.QStatus == true && q.TAnswers.Any())
                    .OrderBy(q=>Guid.NewGuid())
                    .Take(5)//number of questions
                    .Select(q => new
                    {
                        Question = q,
                        Answers = _context.TAnswers
                            .Where(a => a.AQId == q.QId && a.AStatus == true)
                            .ToList()
                    })
                    .ToDictionary(
                        x => x.Question,
                        x => x.Answers
                    );

            List<QuestionWithAnswers> questionsWithAnswers = new();
            foreach (var item in dbResult)
            {
                QuestionWithAnswers questionWithAnswers = new(item.Key.QId, item.Key.QText);
                foreach (var item2 in item.Value)
                    questionWithAnswers.options.Add(new(item2.AId, item2.AText, item2.AIscorrect));
                questionsWithAnswers.Add(questionWithAnswers);
            }

            return questionsWithAnswers;
        }

        [HttpPost("submitQuiz")]
        public IActionResult SubmitQuiz([FromBody] QuizSubmissionModel model)
        {
            var testResult = new TTestResult
            {
                TrCId = model.CandidateId,
                TrTakenDate = DateTime.Now,
                TrTId = model.TestId
                // Calculate and save other result data like score
            };

            // Save test result (assumed to return the generated test_result_id)
            _context.TTestResults.Add(testResult);
            _context.SaveChanges();

            // Save each candidate's answer
            foreach (var answer in model.Answers)
            {
                var candidateAnswer = new TCandidateAnswer
                {
                    CaCId = testResult.TrCId,
                    CaQId = answer.QuestionId,
                    CaAId = answer.AnswerId,
                    CaRId = testResult.TrId
                };
                _context.TCandidateAnswers.Local.Add(candidateAnswer);
            }
            _context.SaveChanges();
                        
            var result = from ca in _context.TCandidateAnswers
                         join tr in _context.TTestResults on ca.CaRId equals tr.TrId
                         join a in (from ans in _context.TAnswers
                                    join q in _context.TQuestions on ans.AQId equals q.QId
                                    select new { ans.AId, ans.AIscorrect })
                                    on ca.CaAId equals a.AId
                         where tr.TrCId == 69 && a.AIscorrect == true
                         select new
                         {
                             ca.CaId,
                             ca.CaAId,
                             a.AIscorrect
                         };

            int resultList = result.ToList().Count;

            float percentage = resultList * 100f / 5f;

            var testResultAgain = _context.TTestResults
                .FirstOrDefault(tr => tr.TrCId == model.CandidateId);

            if (testResult != null)
            {
                testResult.TrScore = (decimal?)percentage;
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("TestResult not found.");
            }

            return Ok(new { message = "Quiz submitted successfully", testResultId = testResult.TrId });
        }

        [HttpGet]
        public async Task<ActionResult<TQuestion>> GetTQuestion(int id)
        {
            if (_context.TQuestions == null)
            {
                return NotFound();
            }
            var tQuestion = await _context.TQuestions.FindAsync(id);
            if (tQuestion == null)
            {
                return NotFound();
            }

            return tQuestion;
        }

        // PUT: api/Quiz/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTQuestion(int id, TQuestion tQuestion)
        {
            if (id != tQuestion.QId)
            {
                return BadRequest();
            }

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

        // POST: api/Quiz
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

        // DELETE: api/Quiz/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTQuestion(int id)
        {
            if (_context.TQuestions == null)
            {
                return NotFound();
            }
            var tQuestion = await _context.TQuestions.FindAsync(id);
            if (tQuestion == null)
            {
                return NotFound();
            }

            _context.TQuestions.Remove(tQuestion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TQuestionExists(int id)
        {
            return (_context.TQuestions?.Any(e => e.QId == id)).GetValueOrDefault();
        }
    }
}
