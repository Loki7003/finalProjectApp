using Microsoft.Data.SqlClient;
using System.Data;

namespace finalProjectMobileApp
{
    public partial class LoginPage : ContentPage
    {

		internal LoginClass login = new LoginClass();

        public LoginPage()
        {
            InitializeComponent();
        }

        public void OnLoginClicked(object sender, EventArgs e)
        {

			string username = UsernameEntry.Text;
			string userpassword = PasswordEntry.Text;

			ConnectionClass connectionClass = new ConnectionClass();
			SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
			connection.Open();
			SqlCommand authenticate = new SqlCommand("dbo.sp_AuthenticateUser", connection);
			authenticate.CommandType = CommandType.StoredProcedure;
			authenticate.Parameters.AddWithValue("@Login", SqlDbType.NVarChar).Value = username;
			authenticate.Parameters.AddWithValue("@Password", SqlDbType.NVarChar).Value = userpassword;
			authenticate.Parameters.Add("@ResponseValue", SqlDbType.Int);
			authenticate.Parameters["@ResponseValue"].Direction = ParameterDirection.Output;
			authenticate.Parameters.Add("@UserId", SqlDbType.Int);
			authenticate.Parameters["@UserId"].Direction = ParameterDirection.Output;
			authenticate.ExecuteNonQuery();
			login.LoginResponse = (Int32)authenticate.Parameters["@ResponseValue"].Value;
			connection.Close();

			if (login.LoginResponse == 0)
			{
				DisplayAlert("Invalid login", "Invalid login or password!\nPleas try again.", "OK");
			}
			else
			{
				login.Id = (Int32)authenticate.Parameters["@UserId"].Value;
				HomePage homePage = new HomePage(login.Id);
				Navigation.PopAsync();
				Navigation.PushAsync(homePage);
			}

			

		}

	}

}
