using finalProjectApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace finalProjectApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private string connectionString = "Data Source=vps-64805245.vps.ovh.net;Initial Catalog=finalProjectDb;User Id=connection_operator;Password=JsMuYtnJ6QwKqE2XXqsM";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public ActionResult Login(string username, string userpassword)
		{
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand authenticate = new SqlCommand("dbo.sp_AuthenticateUser", connection);
			authenticate.CommandType = CommandType.StoredProcedure;
			authenticate.Parameters.AddWithValue("@Login", SqlDbType.NVarChar).Value = username;
			authenticate.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = userpassword;
			authenticate.Parameters.Add("@Response", SqlDbType.Int);
			authenticate.Parameters["@Response"].Direction = ParameterDirection.Output;
			authenticate.Parameters.Add("@UserId", SqlDbType.Int);
			authenticate.Parameters["@UserId"].Direction= ParameterDirection.Output;
            authenticate.ExecuteNonQuery();
			int response = (Int32)authenticate.Parameters["@Response"].Value;
			int userId = (Int32)authenticate.Parameters["@UserId"].Value;
			connection.Close();
			userpassword = null;
			if(response == 0)
			{
				return View(Index());
			}
			else
			{
				connection.Open();
				SqlCommand selectUser = new SqlCommand("SELECT UserId, UserLogin, FirstName, LastName, MailAddress, RoleName FROM dbo.vw_SelectUser WHERE UserId = " + userId, connection);
				UserModel user = new UserModel();
				SqlDataReader reader = selectUser.ExecuteReader();
				if(reader.Read())
				{
					user.Id = (Int32)reader[0];
					user.Username = (String)reader[1];
					user.Name = (String)reader[2];
					user.Lastname = (String)reader[3];
					user.Email = (String)reader[4];
					user.Role = (String)reader[5];
				}
				connection.Close();
				//If user role
                return RedirectToAction("Index", "User", user);
            }
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
