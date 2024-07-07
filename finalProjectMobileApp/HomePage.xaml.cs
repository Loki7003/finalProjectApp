using Microsoft.Data.SqlClient;
using System.Data;

namespace finalProjectMobileApp;

public partial class HomePage : ContentPage
{
	public HomePage(int userId)
	{
		InitializeComponent();
		UserClass user = GetUserData(userId);
		BindingContext = user;

	}

	public UserClass GetUserData(int userId)
	{
		ConnectionClass connectionClass = new ConnectionClass();
		SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
		connection.Open();
		SqlCommand selectUser = new SqlCommand("SELECT UserId, UserLogin, FirstName, LastName, MailAddress, RoleName, PasswordExpired, PasswordChangedOn, UserEnabled FROM dbo.vw_SelectUser WHERE UserId = " + userId, connection);
		UserClass user = new UserClass();
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

		return user;
	}

}