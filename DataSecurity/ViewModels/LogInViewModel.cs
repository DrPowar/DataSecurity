using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataSecurity.Models;
using DataSecurity.Pages;

namespace DataSecurity.ViewModels
{
    public partial class LogInViewModel : ObservableObject
    {
        private readonly Client.Client _client = new();

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _showError;

        [RelayCommand]
        public async Task LogIn()
        {
            try
            {
                Person person = await _client.LogInAsync(Name, Password);

                if (person == null)
                {
                    ShowError = true;
                    return;
                }

                await Shell.Current.GoToAsync($"//{nameof(MainPage)}", new Dictionary<string, object>
                {
                    { nameof(Person), person }
                });
            }
            catch
            {
                ShowError = true;
                return;
            }
        }

        [RelayCommand]
        public void NavigateToRegister()
        {
            Shell.Current.GoToAsync(nameof(RegisterPage));
        }

    }
}
