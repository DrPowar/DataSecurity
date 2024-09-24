using DataSecurity.DTO;
using DataSecurity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataSecurity.Client
{
    internal class Client
    {
        private HttpClient _httpClient;

        private const int Port = 5072;

        internal Client()
        {
            _httpClient = HttpClientFactory.Create();
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            var response = await _httpClient.GetAsync($"http://localhost:{Port}/Person/GetAll");

            return await response.Content.ReadFromJsonAsync<List<Person>>();
        }

        public async Task<Person> LogInAsync(string name, string password)
        {
            var response = await _httpClient.GetAsync($"http://localhost:{Port}/Person/LogIn?name={name}&password={password}");

            Person person = await response.Content.ReadFromJsonAsync<Person>();

            if(person.Id == Guid.Empty)
            {
                return null;
            }
            else
            {
                return person;
            }
        }

        public async Task ChangePasswordAsync(Guid personId, string oldPassword, string newPassword)
        {
            ChangePasswordRequest changePasswordRequest = new ChangePasswordRequest(personId, oldPassword, newPassword);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{Port}/Person/ChangePassword", changePasswordRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task BlockUserAsync(Guid adminId, Guid userId)
        {
            BlockUserRequest blockUserRequest = new BlockUserRequest(adminId, userId);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{Port}/Person/BlockUser", blockUserRequest);
            response.EnsureSuccessStatusCode();
        }
        public async Task RegisterWithPasswordAsync(string name, string password)
        {
            RegisterRequest registerRequest = new(name, password);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{Port}/Person/RegisterWithPassword", registerRequest);
        }
        public async Task RegisterWithoutPasswordAsync(string name)
        {
            RegisterRequest registerRequest = new(name, null);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{Port}/Person/RegisterWithoutPassword", registerRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task RegisterWithPasswordAdminAsync(string name, string password)
        {
            RegisterRequest registerRequest = new(name, password);
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{Port}/Person/RegisterWithPasswordAdmin", registerRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task SwitchSpecialFeatureAsync(Guid adminId)
        {
            var response = await _httpClient.PostAsJsonAsync($"http://localhost:{Port}/Person/SwitchSpecialFeature", adminId);
        }

        public async Task<bool> GetSpecialFeatureInfo()
        {
            var response = await _httpClient.GetAsync($"http://localhost:{Port}/Person/GetSpecialFeatureInfo");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<bool>();

            return result;
        }

    }
}
