using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestSubjectController : ControllerBase
    {
        private readonly string connectionString;

        public TestSubjectController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("TestSubject");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var testSubjectList = new List<TestSubject>();
            while (reader.Read())
            {
                TestSubject ts = new TestSubject();
                ts.Id = Convert.ToInt32(reader["Id"]);
                ts.TestId = Convert.ToInt32(reader["TestId"]);
                ts.SubjectId = Convert.ToInt32(reader["SubjectId"]);
                ts.QuestionMarks = Convert.ToInt32(reader["QuestionMarks"]);
                ts.IsNegativeMarking = Convert.ToBoolean(reader["IsNegativeMarking"]);
                testSubjectList.Add(ts);
            }

            return Ok(testSubjectList);
        }

        [HttpPost]
        public IActionResult Post(TestSubject testSubject)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "TestId, SubjectId, QuestionMarks, IsNegativeMarking";
            string value = "@TestId, @SubjectId, @QuestionMarks, @IsNegativeMarking";
            string query = CRUD.Insert("TestSubject", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@TestId", testSubject.TestId);
            sqlCommand.Parameters.AddWithValue("@SubjectId", testSubject.SubjectId);
            sqlCommand.Parameters.AddWithValue("@QuestionMarks", testSubject.QuestionMarks);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", testSubject.IsNegativeMarking);
            sqlCommand.ExecuteNonQuery();

            return Ok(testSubject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TestSubject testSubject)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "TestId = @TestId, SubjectId = @SubjectId, QuestionMarks = @QuestionMarks, IsNegativeMarking = @IsNegativeMarking";
            string value = "Id = @id";
            string query = CRUD.Update("TestSubject", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@TestId", testSubject.TestId);
            sqlCommand.Parameters.AddWithValue("@SubjectId", testSubject.SubjectId);
            sqlCommand.Parameters.AddWithValue("@QuestionMarks", testSubject.QuestionMarks);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", testSubject.IsNegativeMarking);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @Id";
            string query = CRUD.Delete("TestSubject", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}
