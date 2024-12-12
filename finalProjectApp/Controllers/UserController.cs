using finalProjectApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace finalProjectApp.Controllers
{
	public class UserController : Controller
	{
		private readonly JsonSerializerOptions _options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			WriteIndented = true
		};
		public IActionResult UserMenuRedirect(LoginModel login)
		{
			if (login.Id != -1)
			{
				ConnectionClass connectionClass = new ConnectionClass();
				SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
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
				reader.Close();
				connection.Close();
				if (user.Enabled)
				{
					var userJson = JsonSerializer.Serialize(user, _options);
					HttpContext.Session.SetString("User", userJson);
					return RedirectToAction("UserMenu", "User");
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

		public ActionResult UserMenu()
		{
			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			if (!user.PasswordExpired)
			{
				return View();
			}
			else
			{
				return RedirectToAction("ChangeUserPassword", "User");
			}	
		}

		public IActionResult Logout()
		{
			LoginModel login = new LoginModel();
			HttpContext.Session.Clear();
			return RedirectToAction("Index", "Home", login);
		}

		public IActionResult ChangeUserPassword()
		{
            var userJson = HttpContext.Session.Get("User");
            var user = JsonSerializer.Deserialize<UserModel>(userJson);
			ChangePasswordModel changePassword = new ChangePasswordModel();

			return View(changePassword);
        }

		public ActionResult ChangePassword(string newPassword, string confirmNewPassword)
		{

            ChangePasswordModel changePassword = new ChangePasswordModel();

            var userJson = HttpContext.Session.Get("User");
            var user = JsonSerializer.Deserialize<UserModel>(userJson);

            if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
			{
				changePassword.ChangeResponse = 2;
				return RedirectToAction("ChangeUserPassword", "User", changePassword);
			}
			else if (newPassword != confirmNewPassword)
			{
                changePassword.ChangeResponse = 3;
                return RedirectToAction("ChangeUserPassword", "User", changePassword);
            }
            else
            {
                ConnectionClass connectionClass = new ConnectionClass();
                SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
                connection.Open();
                SqlCommand authenticate = new SqlCommand("dbo.sp_ChangeUserPassword", connection);
                authenticate.CommandType = CommandType.StoredProcedure;
                authenticate.Parameters.AddWithValue("@UserId", SqlDbType.NVarChar).Value = user.Id;
                authenticate.Parameters.AddWithValue("@NewPassword", SqlDbType.NVarChar).Value = newPassword;
                authenticate.Parameters.Add("@ResponseValue", SqlDbType.Int);
                authenticate.Parameters["@ResponseValue"].Direction = ParameterDirection.Output;
                authenticate.ExecuteNonQuery();
                int response = (Int32)authenticate.Parameters["@ResponseValue"].Value;
                connection.Close();

                if (response == 0)
                {
                    changePassword.ChangeResponse = 0;
                    return RedirectToAction("ChangeUserPassword", "User", changePassword);
                }
                else
                {
                    changePassword.ChangeResponse = 1;
					user.PasswordExpired = false;
                    var userJson1 = JsonSerializer.Serialize(user,_options);
                    HttpContext.Session.SetString("User", userJson1);
                    return RedirectToAction("ChangeUserPassword", "User", changePassword);
                }
            }
        }
	}
}
