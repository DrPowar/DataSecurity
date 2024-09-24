using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataSecurity.Models;
using DataSecurity.Pages;

namespace DataSecurity.ViewModels
{
    public partial class ChangePasswordViewModel : ObservableObject, IQueryAttributable
    {
        private readonly Client.Client _client = new();

        private Person _person;

        [ObservableProperty]
        private string _oldPassword;

        [ObservableProperty]
        private string _newPassword;


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.ContainsKey(nameof(Person)))
            {
                _person = query[nameof(Person)] as Person;
            }
        }

        [RelayCommand]
        private async void ChangePassword()
        {
            _client.ChangePasswordAsync(_person.Id, OldPassword, NewPassword);

            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
        }
    }
}
