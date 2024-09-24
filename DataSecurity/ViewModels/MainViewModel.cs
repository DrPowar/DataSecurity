using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataSecurity.Models;
using DataSecurity.Pages;
using System.Collections.ObjectModel;

namespace DataSecurity.ViewModels
{
    internal partial class MainViewModel : ObservableObject, IQueryAttributable
    {
        private Person _person;
        private readonly Client.Client _client = new();

        [ObservableProperty]
        private bool _isAdmin;

        [ObservableProperty]
        private string _specialFeatureStaus = "Turn On";

        [ObservableProperty]
        private ObservableCollection<Person> _people = new();

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if(query.ContainsKey("Person"))
            {
                _person = query["Person"] as Person;
                if(_person.Role.Name == "Admin")
                {
                    IsAdmin = true;
                }
                await GetPeople();
            }

        }

        [RelayCommand]
        private async Task BlockUser(Person person)
        {
            await _client.BlockUserAsync(_person.Id, person.Id);
            await GetPeople();
        }

        [RelayCommand]
        private void NavigateToRegister()
        {
            Shell.Current.GoToAsync("RegisterPage");
        }

        [RelayCommand]
        private async void NavigateToChangePassword()
        {
            await Shell.Current.GoToAsync($"{nameof(ChangePasswordPage)}", new Dictionary<string, object>
                {
                    { nameof(Person), _person }
                });
        }

        private async Task GetPeople()
        {
            _people.Clear();

            List<Person> people = await _client.GetAllPeopleAsync();
            foreach (Person person in people)
            {
                _people.Add(person);
            }
        }

        [RelayCommand]
        private async void SwitchSpecialFeature()
        {
            await _client.SwitchSpecialFeatureAsync(_person.Id);
        }
    }
}
