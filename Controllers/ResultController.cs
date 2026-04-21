using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultController : ControllerBase
    {
        private readonly string connectionString;

        public ResultController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("Result");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var resultsList = new List<Result>();
            while (reader.Read())
            {
                Result result = new Result();
                result.Id = Convert.ToInt32(reader["Id"]);
                result.CandidateId = Convert.ToInt32(reader["CandidateId"]);
                result.TotalMarks = Convert.ToInt32(reader["TotalMarks"]);
                result.Rank_Of_Candidate = Convert.ToInt32(reader["Rank_Of_Candidate"]);
                result.TestId = Convert.ToInt32(reader["TestId"]);
                resultsList.Add(result);
            }

            return Ok(resultsList);
        }

        [HttpPost]
        public IActionResult Post(Result result)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "CandidateId, TotalMarks, Rank_Of_Candidate, TestId";
            string value = "@CandidateId, @TotalMarks, @Rank_Of_Candidate, @TestId";
            string query = CRUD.Insert("Result", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@CandidateId", result.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TotalMarks", result.TotalMarks);
            sqlCommand.Parameters.AddWithValue("@Rank_Of_Candidate", result.Rank_Of_Candidate);
            sqlCommand.Parameters.AddWithValue("@TestId", result.TestId);
            sqlCommand.ExecuteNonQuery();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Result result)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "CandidateId = @CandidateId, TotalMarks = @TotalMarks, Rank_Of_Candidate = @Rank_Of_Candidate, TestId = @TestId";
            string value = "Id = @id";
            string query = CRUD.Update("Result", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@CandidateId", result.CandidateId);
            sqlCommand.Parameters.AddWithValue("@TotalMarks", result.TotalMarks);
            sqlCommand.Parameters.AddWithValue("@Rank_Of_Candidate", result.Rank_Of_Candidate);
            sqlCommand.Parameters.AddWithValue("@TestId", result.TestId);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @Id";
            string query = CRUD.Delete("Result", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}

