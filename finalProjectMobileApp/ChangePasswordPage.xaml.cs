using Azure;
using System.Data;
using Microsoft.Data.SqlClient;

namespace finalProjectMobileApp;

public partial class ChangePasswordPage : ContentPage
{
	public int Id;
	public ChangePasswordPage(bool passwordExpired, int technicianId)
	{
		InitializeComponent();
		Id = technicianId;
		if (passwordExpired)
		{
			returnToHomePage.IsEnabled = false;
		}
	}

	private void ReturnToHomePage_Clicked(object sender, EventArgs e)
	{
		HomePage homePage = new HomePage(Id);
		Navigation.PopAsync();
		Navigation.PushAsync(homePage);
	}

	private void ChangePasswordButton_Clicked(object sender, EventArgs e)
	{
		string newPassword = newPasswordEntry.Text;
		string confirmNewPassword = confirmNewPasswordEntry.Text;
		if (string.IsNullOrEmpty(newPassword) || string.IsNullOrEmpty(confirmNewPassword))
		{
			errorLabel.Text = "Proszę uzupełnić wszystkie pola!";
			errorLabel.IsVisible = true;
		}
		else if (newPassword != confirmNewPassword)
		{
			errorLabel.Text = "Podane hasła nie są zgodne!";
			errorLabel.IsVisible = true;
		}
		else
		{
			errorLabel.Text = "";
			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand authenticate = new SqlCommand("dbo.sp_ChangeTechnicianPassword", connection);
			authenticate.CommandType = CommandType.StoredProcedure;
			authenticate.Parameters.AddWithValue("@TechnicianId", SqlDbType.NVarChar).Value = Id;
			authenticate.Parameters.AddWithValue("@NewPassword", SqlDbType.NVarChar).Value = newPassword;
			authenticate.Parameters.Add("@ResponseValue", SqlDbType.Int);
			authenticate.Parameters["@ResponseValue"].Direction = ParameterDirection.Output;
			authenticate.ExecuteNonQuery();
			int response = (Int32)authenticate.Parameters["@ResponseValue"].Value;
			connection.Close();

			if (response == 0)
			{
				errorLabel.Text = "Nowe hasło nie może być takie same jak poprzednie!";
				errorLabel.IsVisible = true;
			}
			else
			{
				errorLabel.TextColor = Colors.Green;
				errorLabel.Text = "Hasło zostało zmienione!";
				errorLabel.IsVisible = true;
				returnToHomePage.IsEnabled = true;
			}
		}
	}
}