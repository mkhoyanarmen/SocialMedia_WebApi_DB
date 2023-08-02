using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace SocialMedia_BITC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static readonly string StringConnection = "Data Source=DESKTOP-8JP79RT\\SQLEXPRESS;Initial Catalog=SocialMedia;Integrated Security=True";
        SqlConnection con = new SqlConnection(StringConnection);

        [HttpPost]
        public ActionResult AddUser(string fullName, DateTime dateOfBirth, bool isMale)
        {
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = $"insert into Users values ('{fullName}', '{dateOfBirth.Year}-{dateOfBirth.Month}-{dateOfBirth.Day}', {Convert.ToByte(isMale)})";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return Content($"Added User - {fullName} || {dateOfBirth.ToString("dd-MM-yyyy")} || {isMale.ToString()}");
            }
            return BadRequest();
        }

        [HttpGet]
        public ActionResult GetUser(int id)
        {
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = $"select FullName, DateOfBirth, IsMale from Users where Id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Content($"{reader["FullName"]} || {reader["DateOfBirth"]} || {reader["IsMale"]}");
                }
                else
                {
                    con.Close();
                    return Content("No Data Found");
                }
            }
            return BadRequest();
        }

        [HttpGet("AllUsers")]
        public ActionResult GetAllUsers()
        {
            con.Open();
            string content = "";
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from Users";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    content += $"{reader["Id"]}: {reader["FullName"]} || {reader["DateOfBirth"]} || {reader["IsMale"]}\n";
                }
                con.Close();
                return Content(content);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateUser(int id, string fullName)
        {
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = $"update Users set FullName = '{fullName}' where Id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete]
        public ActionResult DeleteUser(int id)
        {
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string content = "";
                string querySelect = $"select FullName, DateOfBirth, IsMale from Users where Id = {id}";
                SqlCommand cmdSelect = new SqlCommand(querySelect, con);
                SqlDataReader reader = cmdSelect.ExecuteReader();

                string queryDelete = $"delete from Users where Id = {id}";
                if (reader.Read())
                {
                    content = $"{reader["FullName"]} || {reader["DateOfBirth"]} || {reader["IsMale"]} //Removed";
                }
                reader.Close();

                SqlCommand cmd = new SqlCommand(queryDelete, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return Content(content);
            }
            return BadRequest();
        }
    }
}
