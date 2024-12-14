using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjectMobileApp
{
	internal class TaskClass
	{
		public int TaskId { get; set; }
		public string TaskSubject { get; set; }
		public string TaskDetails { get; set; }
		public DateTime? TaskCreated { get; set; }
		public string TaskCategory { get; set; }
		public string TaskRequestor { get; set; }
		public string? TaskTechnician { get; set; }
		public string TaskStatus { get; set; }
		public DateTime? TaskClosed { get; set; }
		public int RequestorId { get; set; }
		public string RequestorAddress { get; set; }
	}
}
