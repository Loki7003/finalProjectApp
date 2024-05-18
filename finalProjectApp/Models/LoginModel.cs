using System.ComponentModel.DataAnnotations;

namespace finalProjectApp.Models
{
	public class LoginModel
	{
		public int Id { get; set; } = -1;
		[Required]
		public int LoginResponse { get; set; } = 10;
	}
}
