namespace finalProjectApp.Models
{
	public class AdministrativeCaseModel
	{
		public int CaseId { get; set; }
		public string CaseSubject { get; set; }
		public string CaseDetails { get; set; }
		public DateTime? CaseCreated { get; set; }
		public string CaseRequestor { get; set; }
		public string? AssignedEmployee { get; set; }
		public string CaseStatus { get; set; }
		public DateTime? CaseClosed { get; set; }
		public string? CaseResponse { get; set; }
		public int RequestorId { get; set; }
		public string RequestorAddress { get; set; }
	}
}
