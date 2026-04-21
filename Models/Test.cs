namespace QuizCompetition.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime TestDate { get; set; }
        public bool IsNegativeMarking { get; set; }
        public DateTime GeneratedOn { get; set; }
        public int TotalMarks { get; set; }
        public int Duration { get; set; }

        //public List<TestQuestion> TestQuestions { get; set; }
        //public List<CandidateAttempt> CandidateAttempts { get; set; }
        //public List<Result> Results { get; set; }
        //public List<TestSubject> TestSubjects { get; set; }
    }

}
