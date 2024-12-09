using System.ComponentModel.DataAnnotations;

namespace finalProjectApp.Models
{
    public class ChangePasswordModel
    {
        [Required]
        public int ChangeResponse { get; set; } = 10;
    }
}
