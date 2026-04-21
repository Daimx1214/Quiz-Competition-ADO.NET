using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CandidateAnswerController : ControllerBase
    {
        private readonly string connectionString;
        public CandidateAnswerController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        [HttpGet]
        public IActionResult Get()
        {

            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("CandidateAnswer");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var students = new List<CandidateAnswer>();
            while (reader.Read())
            {
                CandidateAnswer ans = new CandidateAnswer();
                ans.Id = Convert.ToInt32(reader["Id"]);
                ans.CandidateId = Convert.ToInt32(reader["CandidateId"]);
                ans.TestQuestionId = Convert.ToInt32(reader["TestQuestionId"]);
                ans.MarksObtained = Convert.ToInt32(reader["MarksObtained"]);
                ans.IsCorrect = Convert.ToBoolean(reader["IsCorrect"]);
                ans.SelectedOptionId = Convert.ToInt32(reader["SelectedOptionId"]);
                students.Add(ans);
            }
            return Ok(students);
        }
        [HttpPost]
        public IActionResult Post(CandidateAnswer candidateAnswer)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "CandidateId, TestQuestionId, MarksObtained, IsCorrect, SelectedOptionId";
            string value = "@CandidateId, @TestQuestionId, @MarksObtained, @IsCorrect, @SelectedOptionId";
            string query = CRUD.Insert("CandidateAnswer", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CandidateId", candidateAnswer.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TestQuestionId", candidateAnswer.TestQuestionId);
            sqlCommand.Parameters.AddWithValue("@MarksObtained", candidateAnswer.MarksObtained);
            sqlCommand.Parameters.AddWithValue("@IsCorrect", candidateAnswer.IsCorrect);
            sqlCommand.Parameters.AddWithValue("@SelectedOptionId", candidateAnswer.SelectedOptionId);
            sqlCommand.ExecuteNonQuery();

            return Ok(candidateAnswer);
        }

        [HttpPut("{id}")]

        public IActionResult Update(int id, CandidateAnswer candidateAnswer)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "CandidateId = @CandidateId, TestQuestionId = @TestQuestionId,MarksObtained = @MarksObtained, IsCorrect = @IsCorrect, SelectedOptionId = @SelectedOptionId";
            string value = "Id = @Id";

            string query = CRUD.Update("CandidateAnswer", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CandidateId", candidateAnswer.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TestQuestionId", candidateAnswer.TestQuestionId);
            sqlCommand.Parameters.AddWithValue("@MarksObtained", candidateAnswer.MarksObtained);
            sqlCommand.Parameters.AddWithValue("@IsCorrect", candidateAnswer.IsCorrect);
            sqlCommand.Parameters.AddWithValue("@SelectedOptionId", candidateAnswer.SelectedOptionId);
            int row = sqlCommand.ExecuteNonQuery();

            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @id";
            string query = CRUD.Delete("CandidateAnswer", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}
