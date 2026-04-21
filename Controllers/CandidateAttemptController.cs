using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateAttemptController : ControllerBase
    {
        private readonly string connectionString;

        public CandidateAttemptController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("CandidateAttempt");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var attempts = new List<CandidateAttempt>();
            while (reader.Read())
            {
                CandidateAttempt attempt = new CandidateAttempt();
                attempt.Id = Convert.ToInt32(reader["Id"]);
                attempt.TestId = Convert.ToInt32(reader["TestId"]);
                attempt.CandidateId = Convert.ToInt32(reader["CandidateId"]);
                attempt.EndTime = Convert.ToDateTime(reader["EndTime"]);
                attempts.Add(attempt);
            }

            return Ok(attempts);
        }

        [HttpPost]
        public IActionResult Post(CandidateAttempt candidateAttempt)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "TestId, CandidateId, EndTime";
            string value = "@TestId, @CandidateId, @EndTime";
            string query = CRUD.Insert("CandidateAttempt", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@TestId", candidateAttempt.TestId);
            sqlCommand.Parameters.AddWithValue("@CandidateId", candidateAttempt.CandidateId);
            sqlCommand.Parameters.AddWithValue("@EndTime", candidateAttempt.EndTime);
            sqlCommand.ExecuteNonQuery();

            return Ok(candidateAttempt);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CandidateAttempt candidateAttempt)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "TestId = @TestId, CandidateId = @CandidateId, EndTime = @EndTime";
            string value = "Id = @id";
            string query = CRUD.Update("CandidateAttempt", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@TestId", candidateAttempt.TestId);
            sqlCommand.Parameters.AddWithValue("@CandidateId", candidateAttempt.CandidateId);
            sqlCommand.Parameters.AddWithValue("@EndTime", candidateAttempt.EndTime);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @Id";
            string query = CRUD.Delete("CandidateAttempt", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}