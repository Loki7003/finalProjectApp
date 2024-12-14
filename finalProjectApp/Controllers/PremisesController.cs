using finalProjectApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace finalProjectApp.Controllers
{
	public class PremisesController : Controller
	{
		private readonly JsonSerializerOptions _options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			WriteIndented = true
		};
		public ActionResult CreatePremise()
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
		public ActionResult ListPremises()
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
			SqlCommand selectPremises = new SqlCommand("SELECT [PremisId], [PremisStreet], [PremisStaircaseNumber], [PremisApartmentNumber] FROM [finalProjectDB].[dbo].[vw_PremiseDisplay]", connection);
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
			connection.Close();

			premisesJson = JsonSerializer.Serialize(premisesList, _options);
			ViewData["PremisesJson"] = premisesJson;

			return View(user);
		}
		public ActionResult PremiseDetails(int premiseId)
		{
			if (HttpContext.Session.Get("User") == null)
			{
				LoginModel login = new LoginModel();
				return RedirectToAction("Index", "Home", login);
			}

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			PremiseModel premise = new PremiseModel();
			string premiseJson;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand selectPremises = new SqlCommand("SELECT [PremisId], [PremisStreet], [PremisStaircaseNumber], [PremisApartmentNumber], [FirstName], [LastName], [MailAddress] FROM [finalProjectDB].[dbo].[vw_PremiseDisplay] WHERE PremisId = @PremiseId", connection);
			selectPremises.Parameters.AddWithValue("@PremiseId", premiseId);
			SqlDataReader reader = selectPremises.ExecuteReader();
			if (reader.Read())
			{

				premise.PremiseId = reader.GetInt32(0);
				premise.Street = reader.GetString(1);
				premise.Staircase = reader.GetString(2);
				premise.Apartment = reader.GetInt32(3);
				premise.Owner = (reader.IsDBNull(4) ? "" : reader.GetString(4)) + " " + (reader.IsDBNull(5) ? "" : reader.GetString(5));
				premise.OwnerMailAddress = reader.IsDBNull(6) ? "" : reader.GetString(6);

			}
			reader.Close();
			connection.Close();

			premiseJson = JsonSerializer.Serialize(premise, _options);
			ViewData["PremiseJson"] = premiseJson;
			ViewData["PremiseId"] = premiseId;

			return View(user);
		}

		public IActionResult AddPremise(string street, string staircase, int apartment)
		{

			var userJson = HttpContext.Session.Get("User");
			var user = JsonSerializer.Deserialize<UserModel>(userJson);

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand createPremies = new SqlCommand("INSERT INTO dbo.Premises (PremisStreet, PremisStaircaseNumber, PremisApartmentNumber) VALUES (@Street, @Staircase, @Apartment)", connection);
			createPremies.Parameters.AddWithValue("@Street", street);
			createPremies.Parameters.AddWithValue("@Staircase", staircase);
			createPremies.Parameters.AddWithValue("@Apartment", apartment);
			createPremies.ExecuteNonQuery();

			connection.Close();

			return RedirectToAction("ListPremises", "Premises");

		}
	}
}
