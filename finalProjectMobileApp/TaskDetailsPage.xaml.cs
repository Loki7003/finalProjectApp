using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace finalProjectMobileApp;

public partial class TaskDetailsPage : ContentPage
{
	private TaskClass task = new TaskClass();
	private TechnicianClass tech;
	public TaskDetailsPage(int taskId, TechnicianClass technician)
	{
		InitializeComponent();

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

		tech = technician;

		id.Text = $"{task.TaskId}";
		taskSubject.Text = $"{task.TaskSubject}";
		taskRequestor.Text = $"{task.TaskRequestor}";
		taskCreated.Text = $"{task.TaskCreated}";
		taskClosed.Text = $"{task.TaskClosed}";
		taskTechnician.Text = $"{task.TaskTechnician}";
		taskCategory.Text = $"{task.TaskCategory}";
		taskStatus.Text = $"{task.TaskStatus}";
		requestorAddress.Text = $"{task.RequestorAddress}";
		taskDetails.Text = $"{task.TaskDetails}";

		switch (task.TaskStatus)
		{
			case "Nowe":
				updateTask.IsVisible = true;
				updateTask.Text = "Przypisz";
				updateTask.CommandParameter = 2;
				break;
			case "Przypisane":
				updateTask.IsVisible = true;
				updateTask.Text = "W drodze";
				updateTask.CommandParameter = 3;
				break;
			case "W drodze":
				updateTask.IsVisible = true;
				updateTask.Text = "W realizacji";
				updateTask.CommandParameter = 4;
				break;
			case "W realizacji":
				updateTask.IsVisible = true;
				updateTask.Text = "Zamknij";
				updateTask.CommandParameter = 5;
				break;
		}
	}

	private void updateTask_Clicked(object sender, EventArgs e)
	{
		if (sender is Button button && button.CommandParameter is int value)
		{
			if (value == 5)
			{
				ConnectionClass connectionClass = new ConnectionClass();
				SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
				connection.Open();
				SqlCommand updateCaseCommand = new SqlCommand("UPDATE Tasks SET TaskStatus = @TaskStatus, TaskTechnician = @TechId, TaskClosed = GETDATE() WHERE TaskId = @TaskId", connection);
				updateCaseCommand.Parameters.AddWithValue("@TaskStatus", value);
				updateCaseCommand.Parameters.AddWithValue("@TaskId", task.TaskId);
				updateCaseCommand.Parameters.AddWithValue("@TechId", tech.Id);
				updateCaseCommand.ExecuteNonQuery();
				connection.Close();
			}
			else {
				ConnectionClass connectionClass = new ConnectionClass();
				SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
				connection.Open();
				SqlCommand updateCaseCommand = new SqlCommand("UPDATE Tasks SET TaskStatus = @TaskStatus, TaskTechnician = @TechId WHERE TaskId = @TaskId", connection);
				updateCaseCommand.Parameters.AddWithValue("@TaskStatus", value);
				updateCaseCommand.Parameters.AddWithValue("@TaskId", task.TaskId);
				updateCaseCommand.Parameters.AddWithValue("@TechId", tech.Id);
				updateCaseCommand.ExecuteNonQuery();
				connection.Close();
			}
			Navigation.PushAsync(new TaskDetailsPage(task.TaskId, tech));
		}
	}

	private void returnHome_Clicked(object sender, EventArgs e)
	{

		Navigation.PushAsync(new HomePage(tech.Id));

	}
}