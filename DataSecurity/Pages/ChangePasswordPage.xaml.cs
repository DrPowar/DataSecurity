using DataSecurity.ViewModels;

namespace DataSecurity.Pages;

public partial class ChangePasswordPage : ContentPage
{
	public ChangePasswordPage()
	{
		InitializeComponent();
		BindingContext = new ChangePasswordViewModel();
	}
}