using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly string connectionString;

        public SubjectController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("Subject");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var subjects = new List<Subject>();
            while (reader.Read())
            {
                subjects.Add(new Subject
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    IsActive = Convert.ToBoolean(reader["IsActive"])
                });
            }

            return Ok(subjects);
        }

        [HttpPost]
        public IActionResult Post(Subject subject)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Name,Code,IsActive";
            string value = "@Name,@Code,@IsActive";
            string query = CRUD.Insert("Subject", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Name", subject.Name);
            sqlCommand.Parameters.AddWithValue("@Code", subject.Code);
            sqlCommand.Parameters.AddWithValue("@IsActive", subject.IsActive);

            sqlCommand.ExecuteNonQuery();

            return Ok(subject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Subject subject)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Name = @Name,Code = @Code,IsActive = @IsActive";
            string value = "Id = @id";
            string query = CRUD.Update("Subject", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", subject.Name);
            sqlCommand.Parameters.AddWithValue("@Code", subject.Code);
            sqlCommand.Parameters.AddWithValue("@IsActive", subject.IsActive);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @id";
            string query = CRUD.Delete("Subject", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}
