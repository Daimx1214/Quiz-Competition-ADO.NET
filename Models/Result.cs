namespace QuizCompetition.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }

        public int TotalMarks { get; set; }
        public int Rank_Of_Candidate { get; set; }
        public int TestId { get; set; }

        //public Candidate Candidate { get; set; }
        //public Test Test { get; set; }

    }

}
