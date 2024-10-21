namespace JobApplicationPortal.Helpers
{
    public class AnswerOption
    {
        public AnswerOption(int answerId, string answerText, bool isCorrectOption)
        {
            this.answerId = answerId;
            this.answerText = answerText;
            this.isCorrectOption = isCorrectOption;
        }

        public int answerId { get; private set; }
        public string answerText { get; private set; }
        public bool isCorrectOption { get; private set; }
    }
}
