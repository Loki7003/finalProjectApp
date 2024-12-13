using finalProjectApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Runtime.Intrinsics.X86;
using System.Text.Encodings.Web;

namespace finalProjectApp.Controllers
{
	public class UsersManagementController : Controller
	{
		private readonly JsonSerializerOptions _options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			WriteIndented = true
		};
		public ActionResult CreateTechnician()
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

		public ActionResult ManageTechnician()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			List<TechnicianModel> techsList = new List<TechnicianModel>();
			string techsJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectTechnician = new SqlCommand("SELECT [TechnicianId], [TechnicianFirstName], [TechnicianLastName], [TechnicianEnabled], [TechnicianPasswordExpired], [TechnicianLogin], [TechnicianSpecializationsList], [TechnicianPasswordChangedOn] FROM [finalProjectDB].[dbo].[vw_SelectTechnician] ORDER BY TechnicianId", connection);
			SqlDataReader reader = selectTechnician.ExecuteReader();
			while (reader.Read())
			{

				string[] specList = (reader.GetString(6).Split(','));
				int[] specsId = Array.ConvertAll(specList, int.Parse);

				TechnicianModel tech = new TechnicianModel()
				{
					TechId = reader.GetInt32(0),
					TechFirstName = reader.GetString(1),
					TechLastName = reader.GetString(2),
					Enabled = reader.GetBoolean(3),
					PasswordExpired = reader.GetBoolean(4),
					TechLogin = reader.GetString(5),
					SpecializationsId = new List<int>(),
					PasswordChangedOn = reader.GetDateTime(7)
				};

				foreach (int specId in specsId)
				{
					tech.SpecializationsId.Add(specId);
				}

				techsList.Add(tech);
			}
			reader.Close();

			foreach (TechnicianModel tech in techsList)
			{
				tech.SpecializationsName = new List<string>();

				foreach (int specId in tech.SpecializationsId)
				{
					SqlCommand selectSpecializations = new SqlCommand("SELECT [SpecializationName] FROM [dbo].[Specializations] WHERE SpecializationId = @specId", connection);
					selectSpecializations.Parameters.AddWithValue("@specId", specId);
					SqlDataReader reader1 = selectSpecializations.ExecuteReader();
					while (reader1.Read())
					{
						tech.SpecializationsName.Add(reader1.GetString(0));
					}
					reader1.Close();
				}
			
			}			

			techsJson = JsonSerializer.Serialize(techsList, _options);
			connection.Close();

			ViewData["TechsJson"] = techsJson;

			return View(user);
		}

		public ActionResult TechnicianDetails(int techId)
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);
			string techJson;

			TechnicianModel tech = new TechnicianModel();

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectTechnician = new SqlCommand("SELECT [TechnicianId], [TechnicianFirstName], [TechnicianLastName], [TechnicianEnabled], [TechnicianPasswordExpired], [TechnicianLogin], [TechnicianSpecializationsList], [TechnicianPasswordChangedOn] FROM [finalProjectDB].[dbo].[vw_SelectTechnician] WHERE TechnicianId = @techId ORDER BY TechnicianId", connection);
			selectTechnician.Parameters.AddWithValue("@techId", techId);
			SqlDataReader reader = selectTechnician.ExecuteReader();
			if (reader.Read())
			{

				string[] specList = (reader.GetString(6).Split(','));
				int[] specsId = Array.ConvertAll(specList, int.Parse);

				tech.TechId = reader.GetInt32(0);
				tech.TechFirstName = reader.GetString(1);
				tech.TechLastName = reader.GetString(2);
				tech.Enabled = reader.GetBoolean(3);
				tech.PasswordExpired = reader.GetBoolean(4);
				tech.TechLogin = reader.GetString(5);
				tech.SpecializationsId = new List<int>();
				tech.PasswordChangedOn = reader.GetDateTime(7);
				

				foreach (int specId in specsId)
				{
					tech.SpecializationsId.Add(specId);
				}
			}
			else
			{
				return RedirectToAction("ShowAdministrativeCases", "AdminCases");
			}
			reader.Close();
			
			tech.SpecializationsName = new List<string>();

			foreach (int specId in tech.SpecializationsId)
			{
				SqlCommand selectSpecializations = new SqlCommand("SELECT [SpecializationName] FROM [dbo].[Specializations] WHERE SpecializationId = @specId", connection);
				selectSpecializations.Parameters.AddWithValue("@specId", specId);
				SqlDataReader reader1 = selectSpecializations.ExecuteReader();
				while (reader1.Read())
				{
					tech.SpecializationsName.Add(" " + reader1.GetString(0));
				}
				reader1.Close();
			}

			techJson = JsonSerializer.Serialize(tech, _options);

				ViewData["TechId"] = techId;
				ViewData["TechJson"] = techJson;

				return View(user);
		}

		public ActionResult CreateUser()
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

		public ActionResult ManageUser()
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

		[HttpPost]
		public IActionResult AddTechnician(string techFirstName, string techLastName, int[] techSpecs)
		{

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			int?[] specs = new int?[7];

			for (int i = 0; i <= 6; i++)
			{

				if (techSpecs.Length > i)
				{
					specs[i] = techSpecs[i];
				}
				else
				{
					specs[i] = null;
				}
			
			}

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand createTaskCommand = new SqlCommand("dbo.sp_AddTechnician", connection);
			createTaskCommand.CommandType = CommandType.StoredProcedure;
			createTaskCommand.Parameters.AddWithValue("@Firstname", techFirstName);
			createTaskCommand.Parameters.AddWithValue("@Lastname", techLastName);
			createTaskCommand.Parameters.AddWithValue("@Spec", specs[0]);
			createTaskCommand.Parameters.AddWithValue("@Spec2", specs[1] ?? (object)DBNull.Value);
			createTaskCommand.Parameters.AddWithValue("@Spec3", specs[3] ?? (object)DBNull.Value);
			createTaskCommand.Parameters.AddWithValue("@Spec4", specs[4] ?? (object)DBNull.Value);
			createTaskCommand.Parameters.AddWithValue("@Spec5", specs[5] ?? (object)DBNull.Value);
			createTaskCommand.Parameters.AddWithValue("@Spec6", specs[6] ?? (object)DBNull.Value);
			createTaskCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("ManageTechnician", "UsersManagement");
		}

		[HttpPost]
		public IActionResult AddUser(string userFirstName, string userLastName, int userRole, string userMail)
		{

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand createTaskCommand = new SqlCommand("dbo.sp_AddUser", connection);
			createTaskCommand.CommandType = CommandType.StoredProcedure;
			createTaskCommand.Parameters.AddWithValue("@Firstname", userFirstName);
			createTaskCommand.Parameters.AddWithValue("@Lastname", userLastName);
			createTaskCommand.Parameters.AddWithValue("@MailAddress", userMail);
			createTaskCommand.Parameters.AddWithValue("@Role", userRole);
			createTaskCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("ManageUser", "UsersManagement");
		}

		public IActionResult UpdateTechnician(int techId, int status)
		{

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand updateTechnicianCommand = new SqlCommand("UPDATE TechnicianDetails SET TechnicianEnabled = @Status WHERE TechnicianId = @TechId", connection);
			updateTechnicianCommand.Parameters.AddWithValue("@TechId", techId);
			updateTechnicianCommand.Parameters.AddWithValue("@Status", status);
			updateTechnicianCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("TechnicianDetails", "UsersManagement", new { techId = techId });

		}

		public IActionResult ResetTechnicianPassword(int techId)
		{

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand resetTechnicianPasswordCommand = new SqlCommand("sp_ResetTechnicianPassword", connection);
			resetTechnicianPasswordCommand.CommandType = CommandType.StoredProcedure;
			resetTechnicianPasswordCommand.Parameters.AddWithValue("@TechId", techId);
			resetTechnicianPasswordCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("TechnicianDetails", "UsersManagement", new { techId = techId });

		}
	}
}
