using Microsoft.AspNetCore.Mvc;
using QuizCompetition.Models;
using QuizCompetition.Query;
using Microsoft.Data.SqlClient;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly string connectionString;

        public CandidateController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("Candidate");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var candidates = new List<Candidate>();
            while (reader.Read())
            {
                Candidate candidate = new Candidate();
                candidate.Id = Convert.ToInt32(reader["Id"]);
                candidate.Name = reader["Name"].ToString();
                candidate.Contact = reader["Contact"].ToString();
                candidates.Add(candidate);
            }

            return Ok(candidates);
        }

        [HttpPost]
        public IActionResult Post(Candidate candidate)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Name, Contact";
            string value = "@Name, @Contact";
            string query = CRUD.Insert("Candidate", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Name", candidate.Name);
            sqlCommand.Parameters.AddWithValue("@Contact", candidate.Contact);
            sqlCommand.ExecuteNonQuery();

            return Ok(candidate);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Candidate candidate)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Name = @Name, Contact = @Contact";
            string value = "Id = @id";
            string query = CRUD.Update("Candidate", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", candidate.Name);
            sqlCommand.Parameters.AddWithValue("@Contact", candidate.Contact);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @id";
            string query = CRUD.Delete("Candidate", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}