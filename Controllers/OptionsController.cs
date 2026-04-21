using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptionsController : ControllerBase
    {
        private readonly string connectionString;

        public OptionsController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("Options");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var optionsList = new List<Options>();
            while (reader.Read())
            {
                Options option = new Options();
                option.Id = Convert.ToInt32(reader["Id"]);
                option.QuestionId = Convert.ToInt32(reader["QuestionId"]);
                option.OptionName = reader["OptionName"].ToString();
                option.IsCorrect = Convert.ToBoolean(reader["IsCorrect"]);
                optionsList.Add(option);
            }

            return Ok(optionsList);
        }

        [HttpPost]
        public IActionResult Post(Options option)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "QuestionId, OptionName, IsCorrect";
            string value = "@QuestionId, @OptionName, @IsCorrect";
            string query = CRUD.Insert("Options", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@QuestionId", option.QuestionId);
            sqlCommand.Parameters.AddWithValue("@OptionName", option.OptionName);
            sqlCommand.Parameters.AddWithValue("@IsCorrect", option.IsCorrect);
            sqlCommand.ExecuteNonQuery();

            return Ok(option);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Options option)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "QuestionId = @QuestionId, OptionName = @OptionName, IsCorrect = @IsCorrect";
            string value = "Id = @id";
            string query = CRUD.Update("Options", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@QuestionId", option.QuestionId);
            sqlCommand.Parameters.AddWithValue("@OptionName", option.OptionName);
            sqlCommand.Parameters.AddWithValue("@IsCorrect", option.IsCorrect);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @Id";
            string query = CRUD.Delete("Options", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}
