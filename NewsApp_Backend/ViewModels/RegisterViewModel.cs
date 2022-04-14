using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
	public class RegisterViewModel
	{
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email not valid")]
		[Display(Name = "Email")]
		public string Username { get; set; }
		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }
		[Required]
		[Compare("Password", ErrorMessage = "Password not  equal")]
		[DataType(DataType.Password)]
		[Display(Name = "Password Confirm")]
		public string PasswordConfirm { get; set; }
	}
}
