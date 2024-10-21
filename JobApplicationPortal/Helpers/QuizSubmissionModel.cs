namespace JobApplicationPortal.Helpers
{
    public class QuizSubmissionModel
    {
        public int CandidateId { get; set; }
        public int TestId { get; set; }
        public List<SubmittedAnswer> Answers { get; set; }
    }

    public class SubmittedAnswer
    {
        public int QuestionId { get; set; }

        public int AnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }

}