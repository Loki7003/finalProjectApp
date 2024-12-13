namespace finalProjectApp.Models
{
	public class TechnicianModel
	{
		public int TechId { get; set; }
		public string TechFirstName { get; set; }
		public string TechLastName { get; set; }
		public string TechLogin { get; set; }
		public bool Enabled { get; set; }
		public bool PasswordExpired { get; set; }
		public List<int> SpecializationsId { get; set; }
		public List<string> SpecializationsName { get; set; }
		public DateTime PasswordChangedOn { get; set; }
	}
}
