using System.ComponentModel.DataAnnotations;

namespace SharedSilicon.ViewModels;

public class ContactViewModel
{

	[Display(Name = "Full name", Prompt = "Enter your full name", Order = 0)]
	[DataType(DataType.Text)]
	[Required(ErrorMessage = "Full name is required")]

	public string FullName { get; set; } = null!;


	[Display(Name = "Email address", Prompt = "Enter your email address", Order = 1)]
	[DataType(DataType.EmailAddress)]
	[Required(ErrorMessage = "Email address is required")]
	[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address")]

	public string EmailAddress { get; set; } = null!;


	[Display(Name = "Service (optional)", Prompt = "Choose the service you are interested in", Order = 2)]
	[DataType(DataType.Text)]
	public string? Services { get; set; }


	[Display(Name = "Message", Prompt = "Enter your message here...", Order = 3)]
	[DataType(DataType.MultilineText)]
	[Required(ErrorMessage = "Message is required")]
	public string Message { get; set; } = null!;

	public string? Response { get; set; }

}
