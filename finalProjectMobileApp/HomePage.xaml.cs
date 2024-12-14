using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Maui.Controls;

namespace finalProjectMobileApp;

public partial class HomePage : ContentPage
{
	private TechnicianClass tech;
	public HomePage(int technicianId)
	{
		InitializeComponent();
		TechnicianClass technician = GetUserData(technicianId);

		if (technician.PasswordExpired == true)
		{
			ChangePasswordPage changePasswordPage = new ChangePasswordPage(technician.PasswordExpired, technicianId);
			Navigation.PopAsync();
			Navigation.PushAsync(changePasswordPage);
		}

		string message = $"Witaj, {technician.Name} {technician.Lastname}";

		welcomeLabel.Text = message;

		tech = technician;

	}

	public TechnicianClass GetUserData(int userId)
	{
		ConnectionClass connectionClass = new ConnectionClass();
		SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
		TechnicianClass technician = new TechnicianClass();
		connection.Open();
		SqlCommand selectTechnician = new SqlCommand("SELECT [TechnicianId], [TechnicianFirstName], [TechnicianLastName], [TechnicianEnabled], [TechnicianPasswordExpired], [TechnicianLogin], [TechnicianSpecializationsList], [TechnicianPasswordChangedOn] FROM [finalProjectDB].[dbo].[vw_SelectTechnician] WHERE TechnicianId = @UserId", connection);
		selectTechnician.Parameters.AddWithValue("@UserId", userId);
		SqlDataReader reader = selectTechnician.ExecuteReader();
		if (reader.Read())
		{
			string[] specList = (reader.GetString(6).Split(','));
			int[] specsId = Array.ConvertAll(specList, int.Parse);

			technician = new TechnicianClass()
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1),
				Lastname = reader.GetString(2),
				Enabled = reader.GetBoolean(3),
				PasswordExpired = reader.GetBoolean(4),
				Username = reader.GetString(5),
				Specializations = specsId,
				PasswordChangedOn = reader.GetDateTime(7)
			};
		}
		reader.Close();
		connection.Close();

		return technician;
	}

	public void displayUnassignedTasks_Clicked(object sender, EventArgs e)
	{

		UnassignedTasksPage displayUnassignedTasksPage = new UnassignedTasksPage(tech);
		Navigation.PushAsync(displayUnassignedTasksPage);

	}

	public void displayMyTasks_Clicked(object sender, EventArgs e)
	{

		MyTasksPage displayUnassignedTasksPage = new MyTasksPage(tech);
		Navigation.PushAsync(displayUnassignedTasksPage);

	}

	private void changePassword_Clicked(object sender, EventArgs e)
	{

		ChangePasswordPage changePasswordPage = new ChangePasswordPage(tech.PasswordExpired, tech.Id);
		Navigation.PushAsync(changePasswordPage);

	}

	private void logout_Clicked(object sender, EventArgs e)
	{

		LoginPage loginPage = new LoginPage();
		Navigation.PushAsync(loginPage);

	}
}