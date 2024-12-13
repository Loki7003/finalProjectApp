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
		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public LoginModel login = new LoginModel();

		public IActionResult Index(LoginModel login)
		{
			return View(login);
		}

		public IActionResult NotFound()
		{
			Response.StatusCode = 404;
			return View("~/Views/Shared/NotFound.cshtml");
		}
		public IActionResult NotAuthorized()
		{
			Response.StatusCode = 401;
			return View("~/Views/Shared/NotAuthorized.cshtml");
		}

		public IActionResult Privacy()
		{
			return View();
		}
		[HttpPost]
		public ActionResult Login(string username, string userpassword)
		{
			

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand authenticate = new SqlCommand("dbo.sp_AuthenticateUser", connection);
			authenticate.CommandType = CommandType.StoredProcedure;
			authenticate.Parameters.AddWithValue("@Login", SqlDbType.NVarChar).Value = username;
			authenticate.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = userpassword;
			authenticate.Parameters.Add("@ResponseValue", SqlDbType.Int);
			authenticate.Parameters["@ResponseValue"].Direction = ParameterDirection.Output;
			authenticate.Parameters.Add("@UserId", SqlDbType.Int);
			authenticate.Parameters["@UserId"].Direction = ParameterDirection.Output;
			authenticate.ExecuteNonQuery();
			login.LoginResponse = (Int32)authenticate.Parameters["@ResponseValue"].Value;
			connection.Close();

			userpassword = null;
			if (login.LoginResponse == 0)
			{
				return RedirectToAction("Index", "Home", login);
			}
			else
			{
				login.Id = (Int32)authenticate.Parameters["@UserId"].Value;
				return RedirectToAction("UserMenuRedirect", "User", login);
			}
		}
	}
}
