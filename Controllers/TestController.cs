using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using QuizCompetition.Models;
using QuizCompetition.Query;

namespace QuizCompetition.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly string connectionString;

        public TestController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public IActionResult Get()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string query = CRUD.Get("Test");
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = sqlCommand.ExecuteReader();

            var testList = new List<Test>();
            while (reader.Read())
            {
                Test test = new Test();
                test.Id = Convert.ToInt32(reader["Id"]);
                test.Name = reader["Name"].ToString();
                test.TestDate = Convert.ToDateTime(reader["TestDate"]);
                test.IsNegativeMarking = Convert.ToBoolean(reader["IsNegativeMarking"]);
                test.GeneratedOn = Convert.ToDateTime(reader["GeneratedOn"]);
                test.TotalMarks = Convert.ToInt32(reader["TotalMarks"]);
                test.Duration = Convert.ToInt32(reader["Duration"]);
                testList.Add(test);
            }

            return Ok(testList);
        }

        [HttpPost]
        public IActionResult Post(Test test)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Name, TestDate, IsNegativeMarking, GeneratedOn, TotalMarks, Duration";
            string value = "@Name, @TestDate, @IsNegativeMarking, @GeneratedOn, @TotalMarks, @Duration";
            string query = CRUD.Insert("Test", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Name", test.Name);
            sqlCommand.Parameters.AddWithValue("@TestDate", test.TestDate);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", test.IsNegativeMarking);
            sqlCommand.Parameters.AddWithValue("@GeneratedOn", test.GeneratedOn);
            sqlCommand.Parameters.AddWithValue("@TotalMarks", test.TotalMarks);
            sqlCommand.Parameters.AddWithValue("@Duration", test.Duration);
            sqlCommand.ExecuteNonQuery();

            return Ok(test);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Test test)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Name = @Name, TestDate = @TestDate, IsNegativeMarking = @IsNegativeMarking, GeneratedOn = @GeneratedOn, TotalMarks = @TotalMarks, Duration = @Duration";
            string value = "Id = @id";
            string query = CRUD.Update("Test", key, value);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);
            sqlCommand.Parameters.AddWithValue("@Name", test.Name);
            sqlCommand.Parameters.AddWithValue("@TestDate", test.TestDate);
            sqlCommand.Parameters.AddWithValue("@IsNegativeMarking", test.IsNegativeMarking);
            sqlCommand.Parameters.AddWithValue("@GeneratedOn", test.GeneratedOn);
            sqlCommand.Parameters.AddWithValue("@TotalMarks", test.TotalMarks);
            sqlCommand.Parameters.AddWithValue("@Duration", test.Duration);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();

            string key = "Id = @Id";
            string query = CRUD.Delete("Test", key);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@Id", id);

            int row = sqlCommand.ExecuteNonQuery();
            return Ok(row);
        }
    }
}

