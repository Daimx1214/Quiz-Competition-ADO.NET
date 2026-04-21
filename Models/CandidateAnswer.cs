namespace QuizCompetition.Models
{
    public class CandidateAnswer
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int TestQuestionId { get; set; }
        public int MarksObtained { get; set; }
        public bool IsCorrect { get; set; }
        public int SelectedOptionId { get; set; }

        //public Candidate Candidate { get; set; }
        //public TestQuestion TestQuestion { get; set; }
        //public Options SelectedOption { get; set; }
    }

}
