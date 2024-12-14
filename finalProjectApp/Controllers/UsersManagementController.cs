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

			List<PremiseModel> premisesList = new List<PremiseModel>();

			string premisesJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectPremises = new SqlCommand("SELECT [PremisId], [PremisStreet], [PremisStaircaseNumber], [PremisApartmentNumber], [PremisOwner] FROM [finalProjectDB].[dbo].[Premises] WHERE PremisOwner IS NULL", connection);
			SqlDataReader reader = selectPremises.ExecuteReader();
			while (reader.Read())
			{

				PremiseModel premise = new PremiseModel()
				{
					PremiseId = reader.GetInt32(0),
					Street = reader.GetString(1),
					Staircase = reader.GetString(2),
					Apartment = reader.GetInt32(3)
				};

				premisesList.Add(premise);
			}
			reader.Close();

			premisesJson = JsonSerializer.Serialize(premisesList, _options);
			ViewData["PremisesJson"] = premisesJson;

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

			List<UserModel> usersList = new List<UserModel>();
			string usersJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectUsers = new SqlCommand("SELECT [UserId], [FirstName], [LastName], [UserLogin], [RoleName] FROM [finalProjectDB].[dbo].[vw_SelectUser]", connection);
			SqlDataReader reader = selectUsers.ExecuteReader();
			while (reader.Read())
			{

				UserModel managedUser = new UserModel()
				{
					Id = reader.GetInt32(0),
					Name = reader.GetString(1),
					Lastname = reader.GetString(2),
					Username = reader.GetString(3),
					UserRole = reader.GetString(4)
				};

				usersList.Add(managedUser);

			}
			reader.Close();
			connection.Close();

			usersJson = JsonSerializer.Serialize(usersList, _options);
			ViewData["UsersJson"] = usersJson;


			return View(user);
		}

		public ActionResult UserDetails (int userId)
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			string checkedUserJson;

			UserModel checkedUser = new UserModel();

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectUser = new SqlCommand("SELECT [UserId], [FirstName], [LastName], [UserLogin], [RoleName], [UserEnabled], [PasswordChangedOn] FROM [finalProjectDB].[dbo].[vw_SelectUser]", connection);
			SqlDataReader reader = selectUser.ExecuteReader();
			if (reader.Read())
			{

				checkedUser.Id = reader.GetInt32(0);
				checkedUser.Name = reader.GetString(1);
				checkedUser.Lastname = reader.GetString(2);
				checkedUser.Username = reader.GetString(3);
				checkedUser.UserRole = reader.GetString(4);
				checkedUser.Enabled = reader.GetBoolean(5);
				checkedUser.PasswordChangedOn = reader.GetDateTime(6);
				
			}
			reader.Close();
			connection.Close();

			checkedUserJson = JsonSerializer.Serialize(checkedUser, _options);
			ViewData["CheckedUserJson"] = checkedUserJson;
			ViewData["CheckedUserId"] = userId;


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
		public IActionResult AddUser(string userFirstName, string userLastName, int userRole, string userMail, int? userPremise)
		{

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);
			int createdUserId;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand addUserCommand = new SqlCommand("dbo.sp_AddProgramUser", connection);
			addUserCommand.CommandType = CommandType.StoredProcedure;
			addUserCommand.Parameters.AddWithValue("@Firstname", userFirstName);
			addUserCommand.Parameters.AddWithValue("@Lastname", userLastName);
			addUserCommand.Parameters.AddWithValue("@MailAddress", userMail);
			addUserCommand.Parameters.AddWithValue("@Role", userRole);
			addUserCommand.Parameters.Add("@ResponseValue", SqlDbType.Int);
			addUserCommand.Parameters["@ResponseValue"].Direction = ParameterDirection.Output;
			addUserCommand.ExecuteNonQuery();

			if (userPremise != null)
			{

				createdUserId = (Int32)addUserCommand.Parameters["@ResponseValue"].Value;
				SqlCommand updatePremise = new SqlCommand("UPDATE dbo.Premises SET PremisOwner = @UserId WHERE PremisId = @PremiseId",connection);
				updatePremise.Parameters.AddWithValue("@UserId", createdUserId);
				updatePremise.Parameters.AddWithValue("@PremiseId", userPremise);
				updatePremise.ExecuteNonQuery();

			}

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

		public IActionResult UpdateUser(int userId, int status)
		{

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand updateUserCommand = new SqlCommand("UPDATE UsersDetails SET UserEnabled = @Status WHERE UserId = @UserId", connection);
			updateUserCommand.Parameters.AddWithValue("@UserId", userId);
			updateUserCommand.Parameters.AddWithValue("@Status", status);
			updateUserCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("UserDetails", "UsersManagement", new { userId = userId });

		}

		public IActionResult ResetUserPassword(int userId)
		{

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand resetUserPasswordCommand = new SqlCommand("sp_ResetUserPassword", connection);
			resetUserPasswordCommand.CommandType = CommandType.StoredProcedure;
			resetUserPasswordCommand.Parameters.AddWithValue("@UserId", userId);
			resetUserPasswordCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("TechnicianDetails", "UsersManagement", new { userId = userId });

		}
	}
}
