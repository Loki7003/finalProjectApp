using finalProjectApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace finalProjectApp.Controllers
{
	public class TechnicalTasksController : Controller
	{

		private readonly JsonSerializerOptions _options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			WriteIndented = true
		};

		public ActionResult CreateTechnicalTask()
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

		public ActionResult ShowTechnicalTasks()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			List<TechnicalTaskModel> technicalTasksList = new List<TechnicalTaskModel>();
			string tasksJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectTasks = new SqlCommand("SELECT [TaskId] ,[TaskSubject] ,[TaskDetails] ,[TaskRequestor] ,[TaskRequestorName] ,[TaskCreated] ,[TaskClosed] ,[TechnicianName] ,[TaskTechnician] ,[TaskCategory] ,[CategoryName] ,[TaskStatus] ,[StatusName] FROM [finalProjectDB].[dbo].[vw_TaskDisplay] WHERE TaskRequestor = @userId ORDER BY TaskStatus ASC, TaskCreated DESC, TaskId DESC", connection);
			selectTasks.Parameters.AddWithValue("@userId", user.Id);
			SqlDataReader reader = selectTasks.ExecuteReader();
			while (reader.Read())
			{
				TechnicalTaskModel tech = new TechnicalTaskModel()
				{
					TaskId = reader.GetInt32(0),
					TaskSubject = reader.IsDBNull(1) ? null : reader.GetString(1),
					TaskDetails = reader.IsDBNull(2) ? null : reader.GetString(2),
					TaskRequestor = reader.IsDBNull(4) ? null : reader.GetString(4),
					TaskCreated = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
					TaskClosed = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
					TaskTechnician = reader.IsDBNull(7) ? null : reader.GetString(7),
					TaskCategory = reader.IsDBNull(10) ? null : reader.GetString(10),
					TaskStatus = reader.IsDBNull(12) ? null : reader.GetString(12)
				};
				technicalTasksList.Add(tech);
			}
			tasksJson = JsonSerializer.Serialize(technicalTasksList, _options);
			connection.Close();

			ViewData["TasksJson"] = tasksJson;

			return View(user);
		}

		public ActionResult ShowActiveTechnicalTasks()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			List<TechnicalTaskModel> technicalTasksList = new List<TechnicalTaskModel>();
			string tasksJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectTasks = new SqlCommand("SELECT [TaskId] ,[TaskSubject] ,[TaskDetails] ,[TaskRequestor] ,[TaskRequestorName] ,[TaskCreated] ,[TaskClosed] ,[TechnicianName] ,[TaskTechnician] ,[TaskCategory] ,[CategoryName] ,[TaskStatus] ,[StatusName] FROM [finalProjectDB].[dbo].[vw_TaskDisplay] WHERE TaskStatus BETWEEN 1 AND 4 ORDER BY TaskStatus ASC, TaskCreated DESC, TaskId DESC", connection);
			SqlDataReader reader = selectTasks.ExecuteReader();
			while (reader.Read())
			{
				TechnicalTaskModel tech = new TechnicalTaskModel()
				{
					TaskId = reader.GetInt32(0),
					TaskSubject = reader.IsDBNull(1) ? null : reader.GetString(1),
					TaskDetails = reader.IsDBNull(2) ? null : reader.GetString(2),
					TaskRequestor = reader.IsDBNull(4) ? null : reader.GetString(4),
					TaskCreated = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
					TaskClosed = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
					TaskTechnician = reader.IsDBNull(7) ? null : reader.GetString(7),
					TaskCategory = reader.IsDBNull(10) ? null : reader.GetString(10),
					TaskStatus = reader.IsDBNull(12) ? null : reader.GetString(12)
				};
				technicalTasksList.Add(tech);
			}
			tasksJson = JsonSerializer.Serialize(technicalTasksList, _options);
			connection.Close();

			ViewData["TasksJson"] = tasksJson;

			return View(user);
		}

		public ActionResult ShowArchivedTechnicalTasks()
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			List<TechnicalTaskModel> technicalTasksList = new List<TechnicalTaskModel>();
			string tasksJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectTasks = new SqlCommand("SELECT [TaskId] ,[TaskSubject] ,[TaskDetails] ,[TaskRequestor] ,[TaskRequestorName] ,[TaskCreated] ,[TaskClosed] ,[TechnicianName] ,[TaskTechnician] ,[TaskCategory] ,[CategoryName] ,[TaskStatus] ,[StatusName] FROM [finalProjectDB].[dbo].[vw_TaskDisplay] WHERE TaskStatus = 5 ORDER BY TaskStatus ASC, TaskCreated DESC, TaskId DESC", connection);
			SqlDataReader reader = selectTasks.ExecuteReader();
			while (reader.Read())
			{
				TechnicalTaskModel tech = new TechnicalTaskModel()
				{
					TaskId = reader.GetInt32(0),
					TaskSubject = reader.IsDBNull(1) ? null : reader.GetString(1),
					TaskDetails = reader.IsDBNull(2) ? null : reader.GetString(2),
					TaskRequestor = reader.IsDBNull(4) ? null : reader.GetString(4),
					TaskCreated = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
					TaskClosed = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6),
					TaskTechnician = reader.IsDBNull(7) ? null : reader.GetString(7),
					TaskCategory = reader.IsDBNull(10) ? null : reader.GetString(10),
					TaskStatus = reader.IsDBNull(12) ? null : reader.GetString(12)
				};
				technicalTasksList.Add(tech);
			}
			tasksJson = JsonSerializer.Serialize(technicalTasksList, _options);
			connection.Close();

			ViewData["TasksJson"] = tasksJson;

			return View(user);
		}

		public ActionResult TaskDetails(int taskId)
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);
			string taskJson;
			TechnicalTaskModel task = new TechnicalTaskModel();

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectTask = new SqlCommand("SELECT [TaskId] ,[TaskSubject] ,[TaskDetails] ,[TaskRequestor] ,[TaskRequestorName] ,[TaskCreated] ,[TaskClosed] ,[TechnicianName] ,[TaskTechnician] ,[TaskCategory] ,[CategoryName] ,[TaskStatus] ,[StatusName] FROM [finalProjectDB].[dbo].[vw_TaskDisplay] WHERE TaskId = @TaskId", connection);
			selectTask.Parameters.AddWithValue("@TaskId", taskId);
			SqlDataReader reader = selectTask.ExecuteReader();
			if (reader.Read())
			{
				task.TaskId = reader.GetInt32(0);
				task.TaskSubject = reader.IsDBNull(1) ? null : reader.GetString(1);
				task.TaskDetails = reader.IsDBNull(2) ? null : reader.GetString(2);
				task.RequestorId = reader.GetInt32(3);
				task.TaskRequestor = reader.IsDBNull(4) ? null : reader.GetString(4);
				task.TaskCreated = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
				task.TaskClosed = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
				task.TaskTechnician = reader.IsDBNull(7) ? null : reader.GetString(7);
				task.TaskCategory = reader.IsDBNull(10) ? null : reader.GetString(10);
				task.TaskStatus = reader.IsDBNull(12) ? null : reader.GetString(12);
				
			}
			reader.Close();
			SqlCommand selectPremise = new SqlCommand("SELECT PremisStreet, PremisStaircaseNumber, PremisApartmentNumber FROM dbo.vw_PremiseDisplay WHERE PremisOwner = @Requestor", connection);
			selectPremise.Parameters.AddWithValue("@Requestor", task.RequestorId);
			SqlDataReader reader1 = selectPremise.ExecuteReader();
			if (reader1.Read())
			{
				task.RequestorAddress = reader1.GetString(0) + " " + reader1.GetString(1) + "/" + reader1.GetInt32(2).ToString();
			}
			reader1.Close();

			connection.Close();

			taskJson = JsonSerializer.Serialize(task, _options);

				ViewData["TaskId"] = taskId;
				ViewData["TaskJson"] = taskJson;

				return View(user);

		}

		[HttpPost]
		public IActionResult AddTask(string taskSubject, string taskDetails, int taskCategory)
		{
			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand createTaskCommand = new SqlCommand("dbo.sp_CreateTask", connection);
			createTaskCommand.CommandType = CommandType.StoredProcedure;
			createTaskCommand.Parameters.AddWithValue("@UserId", user.Id);
			createTaskCommand.Parameters.AddWithValue("@TaskSubject", taskSubject);
			createTaskCommand.Parameters.AddWithValue("@TaskDetails", taskDetails);
			createTaskCommand.Parameters.AddWithValue("@TaskCategory", taskCategory);
			createTaskCommand.Parameters.Add("@Response", SqlDbType.Int);
			createTaskCommand.Parameters["@Response"].Direction = ParameterDirection.Output;
			createTaskCommand.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("ShowTechnicalTasks", "TechnicalTasks");
		}
	}
}
