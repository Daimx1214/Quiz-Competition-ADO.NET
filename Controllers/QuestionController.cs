using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly string connectionString;

        public QuestionController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("Question");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var questions = new List<Question>();
            while (reader.Read())
            {
                questions.Add(new Question
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Statement = reader["Statement"].ToString(),
                    QuestionTypeId = Convert.ToInt32(reader["QuestionTypeId"]),
                    SubjectId = Convert.ToInt32(reader["SubjectId"])

                });
            }

            return Ok(questions);
        }

        [HttpPost]
        public IActionResult Post(Question question)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Statement, QuestionTypeId,SubjectId";
            string value = "@Statement, @QuestionTypeId,@SubjectId";
            string query = CRUD.Insert("Question", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Statement", question.Statement);
            sqlCommand.Parameters.AddWithValue("@QuestionTypeId", question.QuestionTypeId);
            sqlCommand.Parameters.AddWithValue("@SubjectId", question.SubjectId);
            sqlCommand.ExecuteNonQuery();

            return Ok(question);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Question question)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Statement = @Statement, QuestionTypeId = @QuestionTypeId,SubjectId=@SubjectId";
            string value = "Id = @id";
            string query = CRUD.Update("Question", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Statement", question.Statement);
            sqlCommand.Parameters.AddWithValue("@QuestionTypeId", question.QuestionTypeId);
            sqlCommand.Parameters.AddWithValue("@SubjectId", question.SubjectId);
            
            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @id";
            string query = CRUD.Delete("Question", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}

