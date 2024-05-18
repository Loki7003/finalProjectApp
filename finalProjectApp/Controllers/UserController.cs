using finalProjectApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Text.Json;

namespace finalProjectApp.Controllers
{
	public class UserController : Controller
	{
		private string connectionString = "Data Source=vps-64805245.vps.ovh.net;Initial Catalog=finalProjectDb;User Id=connection_operator;Password=JsMuYtnJ6QwKqE2XXqsM";

		public ActionResult Index(LoginModel login)
		{
			if (login.Id != -1)
			{
				SqlConnection connection = new SqlConnection(connectionString);
				connection.Open();
				SqlCommand selectUser = new SqlCommand("SELECT UserId, UserLogin, FirstName, LastName, MailAddress, RoleName, PasswordExpired, PasswordChangedOn, UserEnabled FROM dbo.vw_SelectUser WHERE UserId = " + login.Id, connection);
				UserModel user = new UserModel();
				SqlDataReader reader = selectUser.ExecuteReader();
				if (reader.Read())
				{
					user.Id = (Int32)reader[0];
					user.Username = (String)reader[1];
					user.Name = (String)reader[2];
					user.Lastname = (String)reader[3];
					user.Email = (String)reader[4];
					user.UserRole = (String)reader[5];
					user.PasswordExpired = (Boolean)reader[6];
					user.PassworedChangedOn = (DateTime)reader[7];
					user.Enabled = (Boolean)reader[8];
				}
				connection.Close();
				if (user.Enabled)
				{
					var userJson = JsonSerializer.Serialize(user);
					HttpContext.Session.SetString("User", userJson);
					return View(user);
				}
				else 
				{
					login.LoginResponse = 2;
					return RedirectToAction("Index", "Home", login);
				}
			}
			else
			{
				login.LoginResponse = 10;
				return RedirectToAction("Index", "Home", login);
			}
		}
		public ActionResult UserAdministration()
		{
			if(HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			return View(user);
		}
		public ActionResult CreateUserAdministration()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			return View(user);
		}
		public ActionResult UserReports()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			return View(user);
		}
		public ActionResult CreateUserReports()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			return View(user);
		}
		public ActionResult Gate()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			return View(user);
		}
		public IActionResult Logout()
		{
			LoginModel login = new LoginModel();
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home", login);
		}
	}
}
