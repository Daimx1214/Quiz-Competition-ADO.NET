namespace QuizCompetition.Models
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int QuestionId { get; set; }

        public int QuestionMarks { get; set; }
        public bool IsNegativeMarking { get; set; }

        //public Test Test { get; set; }
        //public Question Question { get; set; }
        //public List<CandidateAnswer> CandidateAnswers { get; set; }
    }

}
