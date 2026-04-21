namespace QuizCompetition.Models
{
    public class CandidateAttempt
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int CandidateId { get; set; }
        public DateTime EndTime { get; set; }

        //public Test Test { get; set; }
        //public Candidate Candidate { get; set; }
    }

}
