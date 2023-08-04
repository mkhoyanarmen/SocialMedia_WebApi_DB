using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System;
using System.Text;

namespace SocialMedia_BITC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public static readonly string StringConnection = "Data Source=DESKTOP-8JP79RT\\SQLEXPRESS;Initial Catalog=SocialMedia;Integrated Security=True";
        SqlConnection con = new SqlConnection(StringConnection);
        public static string Base64Encode(string Password)
        {
            var textBytes = Encoding.UTF8.GetBytes(Password);
            return Convert.ToBase64String(textBytes);
        }
        public static string Base64Decode(string base64)
        {
            var base64Bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(base64Bytes);
        }
        [HttpPost]
        public ActionResult AddUser(string username, string password, DateTime dateOfBirth, bool isMale)
        {
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = $"insert into Users values ('{Base64Encode(username)}','{Base64Encode(password)}', '{dateOfBirth.Year}-{dateOfBirth.Month}-{dateOfBirth.Day}', {Convert.ToByte(isMale)})";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return Content($"Added User - {username} || {dateOfBirth.ToString("dd-MM-yyyy")} || {isMale}");
            }
            return BadRequest();
        }

        [HttpGet("UserChecking")]
        public bool CheckUser(string username, string password)
        {
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = $"select * from Users where Username = '{Base64Encode(username)}' and [Password] = '{Base64Encode(password)}'";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    con.Close();
                    return true;
                }
            }
            con.Close();
            return false;
        }

        [HttpGet]
        public ActionResult GetUser(int id)
        {
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = $"select Username, [Password], DateOfBirth, IsMale from Users where Id = {id}";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return Content($"{Base64Decode((string)reader["Username"])} || {reader["Password"]} || {reader["DateOfBirth"]} || {reader["IsMale"]}");
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
                    content += $"{reader["Id"]}: {Base64Decode((string)reader["Username"])} || {reader["Password"]} || {reader["DateOfBirth"]} || {reader["IsMale"]}\n";
                }
                con.Close();
                return Content(content);
            }
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateUser(int id, string username)
        {
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string query = $"update Users set Username = '{Base64Encode(username)}' where Id = {id}";
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
                string querySelect = $"select Username, [Password], DateOfBirth, IsMale from Users where Id = {id}";
                SqlCommand cmdSelect = new SqlCommand(querySelect, con);
                SqlDataReader reader = cmdSelect.ExecuteReader();

                string queryDelete = $"delete from Users where Id = {id}";
                if (reader.Read())
                {
                    content = $"{Base64Decode((string)reader["Username"])} || {reader["Password"]} || {reader["DateOfBirth"]} || {reader["IsMale"]} //Removed";
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
