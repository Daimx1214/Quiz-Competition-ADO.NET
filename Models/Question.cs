namespace QuizCompetition.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        public int QuestionTypeId { get; set; }
        public int SubjectId { get; set; }


        //public QuestionType QuestionType { get; set; }

        //public List<Options> Options { get; set; }
        //public List<TestQuestion> TestQuestions { get; set; }
    }

}
