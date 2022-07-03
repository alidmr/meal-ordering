using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace MealOrdering.Client.Utilities
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly AuthenticationState _anonymus;
        private readonly HttpClient _client;

        public AuthStateProvider(ILocalStorageService localStorageService, HttpClient client)
        {
            _localStorageService = localStorageService;
            _client = client;
            _anonymus = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var testapiToken = await _localStorageService.GetItemAsStringAsync("token");

            var apiToken = await _localStorageService.GetItemAsync<string>("token");

            if (string.IsNullOrEmpty(apiToken))
                return _anonymus;

            string email = await _localStorageService.GetItemAsStringAsync("email");

            var cp = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email)
                }, "JwtAuthType"));

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiToken);

            return new AuthenticationState(cp);
        }

        public void NotifyUserLogin(String email)
        {
            var cp = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }, "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(cp));

            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymus);
            NotifyAuthenticationStateChanged(authState);
        }

    }
}
