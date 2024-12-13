using finalProjectApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace finalProjectApp.Controllers
{
	public class AdminCasesController : Controller
	{
		private readonly JsonSerializerOptions _options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			WriteIndented = true
		};

		public ActionResult CreateAdministrativeCase()
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

		public ActionResult ShowAdministrativeCases()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			List<AdministrativeCaseModel> administrativeCasesList = new List<AdministrativeCaseModel>();
			string casesJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectCases = new SqlCommand("SELECT [CaseId], [CaseSubject], [CaseDetails], [CaseCreated], [CaseRequestorName], [AssignedEmployee], [CaseResponse], [CaseClosed], [StatusName] FROM [finalProjectDB].[dbo].[vw_AdminstrativeCaseDisplay] WHERE CaseRequestor = @userId ORDER BY CaseStatus ASC, CaseCreated DESC, CaseId DESC", connection);
			selectCases.Parameters.AddWithValue("@userId", user.Id);
			SqlDataReader reader = selectCases.ExecuteReader();
			while (reader.Read())
			{
				AdministrativeCaseModel admin = new AdministrativeCaseModel()
				{
					CaseId = reader.GetInt32(0),
					CaseSubject = reader.IsDBNull(1) ? null : reader.GetString(1),
					CaseDetails = reader.IsDBNull(2) ? null : reader.GetString(2),
					CaseCreated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
					CaseRequestor = reader.IsDBNull(4) ? null : reader.GetString(4),
					AssignedEmployee = reader.IsDBNull(5) ? null : reader.GetString(5),
					CaseResponse = reader.IsDBNull(6) ? null : reader.GetString(6),
					CaseClosed = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
					CaseStatus = reader.IsDBNull(8) ? null : reader.GetString(8)
				};
				administrativeCasesList.Add(admin);
			}
			casesJson = JsonSerializer.Serialize(administrativeCasesList,_options);
			connection.Close();

			ViewData["CasesJson"] = casesJson;

			return View(user);
		}

		public ActionResult ShowActiveAdministrativeCases()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			List<AdministrativeCaseModel> administrativeCasesList = new List<AdministrativeCaseModel>();
			string casesJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectCases = new SqlCommand("SELECT [CaseId], [CaseSubject], [CaseDetails], [CaseCreated], [CaseRequestorName], [AssignedEmployee], [CaseResponse], [CaseClosed], [StatusName] FROM [finalProjectDB].[dbo].[vw_AdminstrativeCaseDisplay] WHERE CaseStatus BETWEEN 1 AND 3 ORDER BY CaseStatus ASC, CaseCreated DESC, CaseId DESC", connection);
			selectCases.Parameters.AddWithValue("@userId", user.Id);
			SqlDataReader reader = selectCases.ExecuteReader();
			while (reader.Read())
			{
				AdministrativeCaseModel admin = new AdministrativeCaseModel()
				{
					CaseId = reader.GetInt32(0),
					CaseSubject = reader.IsDBNull(1) ? null : reader.GetString(1),
					CaseDetails = reader.IsDBNull(2) ? null : reader.GetString(2),
					CaseCreated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
					CaseRequestor = reader.IsDBNull(4) ? null : reader.GetString(4),
					AssignedEmployee = reader.IsDBNull(5) ? null : reader.GetString(5),
					CaseResponse = reader.IsDBNull(6) ? null : reader.GetString(6),
					CaseClosed = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
					CaseStatus = reader.IsDBNull(8) ? null : reader.GetString(8)
				};
				administrativeCasesList.Add(admin);
			}
			casesJson = JsonSerializer.Serialize(administrativeCasesList, _options);
			connection.Close();

			ViewData["CasesJson"] = casesJson;

			return View(user);
		}

		public ActionResult ShowArchivedAdministrativeCases()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			List<AdministrativeCaseModel> administrativeCasesList = new List<AdministrativeCaseModel>();
			string casesJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectCases = new SqlCommand("SELECT [CaseId], [CaseSubject], [CaseDetails], [CaseCreated], [CaseRequestorName], [AssignedEmployee], [CaseResponse], [CaseClosed], [StatusName] FROM [finalProjectDB].[dbo].[vw_AdminstrativeCaseDisplay] WHERE CaseStatus BETWEEN 4 AND 5 ORDER BY CaseStatus ASC, CaseCreated DESC, CaseId DESC", connection);
			selectCases.Parameters.AddWithValue("@userId", user.Id);
			SqlDataReader reader = selectCases.ExecuteReader();
			while (reader.Read())
			{
				AdministrativeCaseModel admin = new AdministrativeCaseModel()
				{
					CaseId = reader.GetInt32(0),
					CaseSubject = reader.IsDBNull(1) ? null : reader.GetString(1),
					CaseDetails = reader.IsDBNull(2) ? null : reader.GetString(2),
					CaseCreated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
					CaseRequestor = reader.IsDBNull(4) ? null : reader.GetString(4),
					AssignedEmployee = reader.IsDBNull(5) ? null : reader.GetString(5),
					CaseResponse = reader.IsDBNull(6) ? null : reader.GetString(6),
					CaseClosed = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
					CaseStatus = reader.IsDBNull(8) ? null : reader.GetString(8)
				};
				administrativeCasesList.Add(admin);
			}
			casesJson = JsonSerializer.Serialize(administrativeCasesList, _options);
			connection.Close();

			ViewData["CasesJson"] = casesJson;

			return View(user);
		}

		public ActionResult CaseDetails (int caseId)
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);
			string caseJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectCase = new SqlCommand("SELECT [CaseId], [CaseSubject], [CaseDetails], [CaseCreated], [CaseRequestorName], [AssignedEmployee], [CaseResponse], [CaseClosed], [StatusName] FROM [finalProjectDB].[dbo].[vw_AdminstrativeCaseDisplay] WHERE CaseId = @caseId", connection);
			selectCase.Parameters.AddWithValue("@caseId", caseId);
			SqlDataReader reader = selectCase.ExecuteReader();
			if (reader.Read())
			{
				AdministrativeCaseModel admin = new AdministrativeCaseModel()
				{
					CaseId = reader.GetInt32(0),
					CaseSubject = reader.IsDBNull(1) ? null : reader.GetString(1),
					CaseDetails = reader.IsDBNull(2) ? null : reader.GetString(2),
					CaseCreated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3),
					CaseRequestor = reader.IsDBNull(4) ? null : reader.GetString(4),
					AssignedEmployee = reader.IsDBNull(5) ? null : reader.GetString(5),
					CaseResponse = reader.IsDBNull(6) ? null : reader.GetString(6),
					CaseClosed = reader.IsDBNull(7) ? (DateTime?)null : reader.GetDateTime(7),
					CaseStatus = reader.IsDBNull(8) ? null : reader.GetString(8)
				};

				caseJson = JsonSerializer.Serialize(admin, _options);

				ViewData["CaseId"] = caseId;
				ViewData["CaseJson"] = caseJson;

				return View(user);
			}
			else
			{
				return RedirectToAction("ShowAdministrativeCases", "AdminCases");
			}
			
		}

		[HttpPost]
		public IActionResult AddCase(string caseSubject, string caseDetails)
		{
			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand createCaseCommand = new SqlCommand("dbo.sp_CreateCase", connection);
			createCaseCommand.CommandType = CommandType.StoredProcedure;
			createCaseCommand.Parameters.AddWithValue("@UserId", user.Id);
			createCaseCommand.Parameters.AddWithValue("@CaseSubject", caseSubject);
			createCaseCommand.Parameters.AddWithValue("@CaseDetails", caseDetails);
			createCaseCommand.Parameters.Add("@Response", SqlDbType.Int);
			createCaseCommand.Parameters["@Response"].Direction = ParameterDirection.Output;
			createCaseCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("ShowAdministrativeCases", "AdminCases");
		}

		[HttpGet]
		public IActionResult UpdateCase(int caseId, int caseStatus, int userId)
		{
			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand updateCaseCommand = new SqlCommand("UPDATE AdministrativeCases SET CaseStatus = @caseStatus, AssignedEmployee = @userId WHERE CaseId = @caseId", connection);
			updateCaseCommand.Parameters.AddWithValue("@caseStatus", caseStatus);
			updateCaseCommand.Parameters.AddWithValue("@caseId", caseId);
			updateCaseCommand.Parameters.AddWithValue("@userId", userId);
			updateCaseCommand.ExecuteNonQuery();
			connection.Close();

			return RedirectToAction("CaseDetails", "AdminCases", new { caseId = caseId });
		}

		[HttpGet]
		public IActionResult CloseCase(int caseId, int caseStatus, string caseResponse, int userId)
		{
			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand updateCaseCommand = new SqlCommand("UPDATE AdministrativeCases SET CaseStatus = @caseStatus, CaseResponse = @caseResponse, AssignedEmployee = @userId, CaseClosed=GETDATE() WHERE CaseId = @caseId", connection);
			updateCaseCommand.Parameters.AddWithValue("@caseStatus", caseStatus);
			updateCaseCommand.Parameters.AddWithValue("@caseResponse", caseResponse);
			updateCaseCommand.Parameters.AddWithValue("@caseId", caseId);
			updateCaseCommand.Parameters.AddWithValue("@userId", userId);
			updateCaseCommand.ExecuteNonQuery();
			connection.Close();

			return RedirectToAction("CaseDetails", "AdminCases", new { caseId = caseId });
		}
	}
}
