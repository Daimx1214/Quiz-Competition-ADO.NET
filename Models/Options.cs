namespace QuizCompetition.Models
{
    public class Options
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string OptionName { get; set; }
        public bool IsCorrect { get; set; }

        //public Question Question { get; set; }
    }

}
