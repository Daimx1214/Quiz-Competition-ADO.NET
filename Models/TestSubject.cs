using static System.Net.Mime.MediaTypeNames;

namespace QuizCompetition.Models
{
    public class TestSubject
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int SubjectId { get; set; }
        public int QuestionMarks { get; set; }
        public bool IsNegativeMarking { get; set; }

        //public Test Test { get; set; }
        //public Subject Subject { get; set; }
    }

}
