using finalProjectApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

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
					HttpContext.Session.SetString("UserName", user.Username);
					HttpContext.Session.SetInt32("UserId", user.Id);
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

		public ActionResult LogOut()
		{
			LoginModel login = new LoginModel();
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home", login);
		}

		// GET: UserController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: UserController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: UserController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UserController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: UserController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: UserController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: UserController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
