using JobApplicationPortal.Controllers;

namespace JobApplicationPortal.Helpers
{
    public class QuestionWithAnswers
    {
        public QuestionWithAnswers(int questionId, string questionText)
        {
            this.questionId = questionId;
            this.questionText = questionText;
            options = new List<AnswerOption>();
        }

        public int questionId { get; private set; }
        public string questionText { get; private set; }
        public List<AnswerOption> options { get; private set; }
    }
}
