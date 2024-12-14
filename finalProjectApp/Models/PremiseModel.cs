namespace finalProjectApp.Models
{
	public class PremiseModel
	{
		public int PremiseId { get; set; }
		public string Street { get; set; }
		public string Staircase {  get; set; }
		public int Apartment { get; set; }
		public string? Owner { get; set; }
		public string? OwnerMailAddress { get; set; }
	}
}
