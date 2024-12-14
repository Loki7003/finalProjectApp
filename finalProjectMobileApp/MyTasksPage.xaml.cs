using Microsoft.Data.SqlClient;

namespace finalProjectMobileApp;

public partial class MyTasksPage : ContentPage
{
	private TechnicianClass tech;
	public MyTasksPage(TechnicianClass technician)
	{
		InitializeComponent();

		List<TaskClass> tasksList = new List<TaskClass>();

		ConnectionClass connectionClass = new ConnectionClass();
		SqlConnection connection = new SqlConnection(connectionClass.ConnectionString);
		connection.Open();
		foreach (int spec in technician.Specializations)
		{
			SqlCommand selectTasks = new SqlCommand("SELECT [TaskId] ,[TaskSubject] ,[TaskDetails] ,[TaskRequestor] ,[TaskRequestorName] ,[TaskCreated] ,[TaskClosed] ,[TechnicianName] ,[TaskTechnician] ,[TaskCategory] ,[CategoryName] ,[TaskStatus] ,[StatusName] FROM [finalProjectDB].[dbo].[vw_TaskDisplay] WHERE TaskTechnician = @UserId ORDER BY TaskStatus ASC, TaskCreated DESC, TaskId DESC", connection);
			selectTasks.Parameters.AddWithValue("@UserId", technician.Id);
			SqlDataReader reader = selectTasks.ExecuteReader();
			while (reader.Read())
			{
				TaskClass task = new TaskClass()
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
				tasksList.Add(task);
			}

			tech = technician;

			if (tasksList.Count == 0)
			{
				var label = new Label
				{
					Text = "Brak przypisanych zgłoszeń",
					FontSize = 40,
					Margin = new Thickness(10, 10),
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center,
					LineBreakMode = LineBreakMode.WordWrap
				};

				ButtonsContainer.Children.Add(label);
			}
			else
			{
				foreach (var task in tasksList)
				{
					var taskButton = new Button
					{
						Text = $"{task.TaskSubject}",
						FontSize = 20,
						Margin = new Thickness(10, 10)
					};

					taskButton.CommandParameter = task.TaskId;
					taskButton.Clicked += TaskButton_Clicked;

					ButtonsContainer.Children.Add(taskButton);
				}
			}
		}
	}
	public async void TaskButton_Clicked(object sender, EventArgs e)
	{
		if (sender is Button button && button.CommandParameter is int value)
		{
			await Navigation.PushAsync(new TaskDetailsPage(value, tech));
		}
	}
}