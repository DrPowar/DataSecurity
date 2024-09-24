using DataSecurity.ViewModels;

namespace DataSecurity.Pages;

public partial class LogInPage : ContentPage
{
	public LogInPage()
	{
		InitializeComponent();
		BindingContext = new LogInViewModel();
	}
}