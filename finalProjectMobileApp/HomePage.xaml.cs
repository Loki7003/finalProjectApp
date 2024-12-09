using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Maui.Controls;

namespace finalProjectMobileApp;

public partial class HomePage : ContentPage
{
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

	}

	public TechnicianClass GetUserData(int userId)
	{
		ConnectionClass connectionClass = new ConnectionClass();
		SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
		connection.Open();
		SqlCommand selectUser = new SqlCommand("SELECT TechnicianId, TechnicianLogin, TechnicianFirstName, TechnicianLastName, TechnicianPasswordExpired, TechnicianPasswordChangedOn, TechnicianEnabled FROM dbo.vw_SelectTechnician WHERE TechnicianId = " + userId, connection);
		TechnicianClass technician = new TechnicianClass();
		SqlDataReader reader = selectUser.ExecuteReader();
		if (reader.Read())
		{
			technician.Id = (Int32)reader[0];
			technician.Username = (String)reader[1];
			technician.Name = (String)reader[2];
			technician.Lastname = (String)reader[3];
			technician.PasswordExpired = (Boolean)reader[4];
			technician.PassworedChangedOn = (DateTime)reader[5];
			technician.Enabled = (Boolean)reader[6];
		}
		connection.Close();

		return technician;
	}
}