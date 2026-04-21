using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionTypeController : ControllerBase
    {
        private readonly string connectionString;

        public QuestionTypeController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("QuestionType");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var typesList = new List<QuestionType>();
            while (reader.Read())
            {
                QuestionType type = new QuestionType();
                type.Id = Convert.ToInt32(reader["Id"]);
                type.Type = reader["Type"].ToString();
                typesList.Add(type);
            }

            return Ok(typesList);
        }

        [HttpPost]
        public IActionResult Post(QuestionType questionType)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Type";
            string value = "@Type";
            string query = CRUD.Insert("QuestionType", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Type", questionType.Type);
            sqlCommand.ExecuteNonQuery();

            return Ok(questionType);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, QuestionType questionType)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Type = @Type";
            string value = "Id = @id";
            string query = CRUD.Update("QuestionType", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Type", questionType.Type);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @Id";
            string query = CRUD.Delete("QuestionType", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}
