using System.ComponentModel.DataAnnotations;

namespace NewsApp.ViewModels
{
	public class LoginViewModel
	{
		[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email not valid")]
		public string Username { get; set; }

		[Required(ErrorMessage = "password not specified")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}