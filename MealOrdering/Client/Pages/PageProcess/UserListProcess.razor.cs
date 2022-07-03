using System.Net.Http.Json;
using MealOrdering.Client.Utilities;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;

namespace MealOrdering.Client.Pages.PageProcess
{
    public class UserListProcess : ComponentBase
    {
        [Inject]
        public HttpClient Client { get; set; }

        [Inject]
        ModalManager ModalManager { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        protected List<UserDto> userList = new List<UserDto>();

        protected override async Task OnInitializedAsync()
        {
            await LoadList();
        }

        protected void GoToUpdateUserPage(Guid userId)
        {
            NavigationManager.NavigateTo("/users/edit/" + userId);
        }

        protected async Task DeleteUser(Guid userId)
        {
            var confirmed = await ModalManager.ConfirmationAsync("Confirmation", "User will be deleted. Are you sure");
            if (!confirmed) return;

            try
            {
                var deleted = await Client.PostGetServiceResponseAsync<bool, Guid>("api/User/Delete", userId, true);

                await LoadList();
            }
            catch (ApiException exception)
            {
                await ModalManager.ShowMessageAsync("User deletion error", exception.Message);
            }
            catch (Exception exception)
            {
                await ModalManager.ShowMessageAsync("User deletion error", exception.Message);
            }
        }

        protected void GoToCreateUserPage()
        {
            NavigationManager.NavigateTo("/users/add");
        }

        protected async Task LoadList()
        {
            //var serviceResponse = await Client.GetFromJsonAsync<ServiceResponse<List<UserDto>>>("api/User/Users");

            try
            {
                userList = await Client.GetServiceResponseAsync<List<UserDto>>("api/User/Users");
            }
            catch (ApiException exception)
            {
                ModalManager.ShowMessageAsync("Api Exception", exception.Message);
            }
            catch (Exception exception)
            {
                ModalManager.ShowMessageAsync("Exception", exception.Message);
            }
        }
    }
}
