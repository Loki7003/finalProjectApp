namespace finalProjectApp.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public string Email { get; set; }
		public string UserRole {  get; set; }
		public bool Enabled { get; set; }
		public bool PasswordExpired { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime PassworedChangedOn { get; set; }
		public DateTime UserDisabledOn { get; set; }
	}
}
