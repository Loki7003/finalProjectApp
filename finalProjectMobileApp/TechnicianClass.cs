using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjectMobileApp
{
	public class TechnicianClass
	{
		public int Id { get; set; } = -1;
		public string Username { get; set; }
		public string Name { get; set; }
		public string Lastname { get; set; }
		public bool Enabled { get; set; }
		public bool PasswordExpired { get; set; }
		public DateTime PassworedChangedOn { get; set; }
	}
}
