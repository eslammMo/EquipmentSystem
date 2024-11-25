using EquipmentsSystem.Shared.Models;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http.Json;
using static MudBlazor.Colors;
using System.Xml.Linq;

namespace EquipmentsSystem.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword user)
        {
            var result = await _http.PostAsJsonAsync("api/auth/change-password", user);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }

        public async Task<ServiceResponse<bool>> DeleteUser(int obj)
        {
            // Assume `obj.Id` uniquely identifies the user
            var response = await _http.DeleteAsync($"api/auth/DeleteUser?obj={obj}");
            return await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
        }


        public async Task<PagingResponse<List<UserView>>> GetUsers(string email, int currentPageNumber = 1, int pageSize = 10)
        {
            var result =
               await _http.GetFromJsonAsync<PagingResponse<List<UserView>>>($"api/auth/GetUsers" +
               $"?currentPageNumber={currentPageNumber}&" +
               $"pageSize={pageSize}&" +
               $"email= {email}"
                  );
            return result;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
        }

        public async Task<ServiceResponse<int>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
        }

        public async Task<ServiceResponse<UserView>> UpdateUser(UserView obj)
        {
            var result = await _http.PostAsJsonAsync("api/auth/UpdateUser", obj);
            return await result.Content.ReadFromJsonAsync<ServiceResponse<UserView>>();
        }
    }
}
