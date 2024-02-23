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

		//[HttpPost]
		//public ActionResult Index(LoginModel login)
		//{
		//	return View(login);
		//}

		public IActionResult Privacy()
		{
			return View();
		}

		public ActionResult Login(string username, string userpassword)
		{
			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
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
			login.LoginResponse = (Int32)authenticate.Parameters["@Response"].Value;
			
			userpassword = null;
			if(login.LoginResponse == 0)
			{

				return RedirectToAction("Index", "Home",login);
			}
			else
			{
				login.Id = (Int32)authenticate.Parameters["@UserId"].Value;
				return RedirectToAction("Index", "User", login);
			}
			connection.Close();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
