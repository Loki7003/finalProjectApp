using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjectMobileApp
{
	internal class LoginClass
	{
		public int Id { get; set; } = -1;
		[Required]
		public int LoginResponse { get; set; } = 10;
	}
}
