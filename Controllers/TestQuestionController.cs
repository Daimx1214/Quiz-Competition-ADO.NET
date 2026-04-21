using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestQuestionController : ControllerBase
    {
        private readonly string connectionString;

        public TestQuestionController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("TestQuestion");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var testQuestionList = new List<TestQuestion>();
            while (reader.Read())
            {
                TestQuestion tq = new TestQuestion();
                tq.Id = Convert.ToInt32(reader["Id"]);
                tq.TestId = Convert.ToInt32(reader["TestId"]);
                tq.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                tq.QuestionMarks = Convert.ToInt32(reader["QuestionMarks"]);
                tq.IsNegativeMarking = Convert.ToBoolean(reader["IsNegativeMarking"]);
                testQuestionList.Add(tq);
            }

            return Ok(testQuestionList);
        }

        [HttpPost]
        public IActionResult Post(TestQuestion testQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "TestId, QuestionId, QuestionMarks, IsNegativeMarking";
            string value = "@TestId, @QuestionId, @QuestionMarks, @IsNegativeMarking";
            string query = CRUD.Insert("TestQuestion", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@TestId", testQuestion.TestId);
            sqlCommand.Parameters.AddWithValue("@QuestionId", testQuestion.QuestionId);
            sqlCommand.Parameters.AddWithValue("@QuestionMarks", testQuestion.QuestionMarks);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", testQuestion.IsNegativeMarking);
            sqlCommand.ExecuteNonQuery();

            return Ok(testQuestion);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TestQuestion testQuestion)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "TestId = @TestId, QuestionId = @QuestionId, QuestionMarks = @QuestionMarks, IsNegativeMarking = @IsNegativeMarking";
            string value = "Id = @id";
            string query = CRUD.Update("TestQuestion", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@TestId", testQuestion.TestId);
            sqlCommand.Parameters.AddWithValue("@QuestionId", testQuestion.QuestionId);
            sqlCommand.Parameters.AddWithValue("@QuestionMarks", testQuestion.QuestionMarks);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", testQuestion.IsNegativeMarking);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @Id";
            string query = CRUD.Delete("TestQuestion", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}
