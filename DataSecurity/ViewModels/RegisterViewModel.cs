using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataSecurity.Pages;

namespace DataSecurity.ViewModels
{
    public partial class RegisterViewModel : ObservableObject
    {
        private readonly Client.Client _client = new();

        [ObservableProperty]
        private string _name;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _showErrorMessage;

        [ObservableProperty]
        private string _errorMessage = string.Empty;

        [RelayCommand]
        public async Task Register()
        {
            if(await _client.GetSpecialFeatureInfo())
            {
                if(!HasUniqueCharacters(Password))
                {
                    ShowErrorMessage = true;
                    ErrorMessage = "Password contains repeated characters";
                    return;
                }
            }
            
            if(string.IsNullOrWhiteSpace(Password))
            {
                await _client.RegisterWithoutPasswordAsync(Name);

                NavigateToLogIn();

            }
            else
            {
                await _client.RegisterWithPasswordAsync(Name, Password);

                NavigateToLogIn();
            }
        }

        [RelayCommand]
        public void NavigateToLogIn()
        {
            Shell.Current.GoToAsync(nameof(LogInPage));
        }

        private static bool HasUniqueCharacters(string input)
        {
            return input.Length == input.Distinct().Count();
        }
    }
}
