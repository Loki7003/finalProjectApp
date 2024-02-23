using System.ComponentModel.DataAnnotations;

namespace finalProjectApp.Models
{
	public class LoginModel
	{
		public int Id { get; set; }
		[Required]
		public int LoginResponse { get; set; } = 10;
	}
}
