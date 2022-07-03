using System.Net.Http.Json;
using Blazored.LocalStorage;
using MealOrdering.Client.Utilities;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace MealOrdering.Client.Pages.PageProcess
{
    public class SupplierBusiness : ComponentBase
    {

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager UrlNavigationManager { get; set; }

        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        [Inject]
        protected ISyncLocalStorageService LocalStorageSync { get; set; }

        [Inject]
        ModalManager ModalManager { get; set; }

        [Inject]
        IJSRuntime jsRuntime { get; set; }



        protected List<SupplierDto> SupplierList;

        protected override async Task OnInitializedAsync()
        {
            await ReLoadList();
        }

        public void GoCreateSupplier()
        {
            UrlNavigationManager.NavigateTo("/suppliers/add");
        }

        public void GoEditOrder(Guid SupplierId)
        {
            UrlNavigationManager.NavigateTo("/suppliers/edit/" + SupplierId.ToString());

        }

        public async Task ReLoadList()
        {
            var res = await Http.GetFromJsonAsync<ServiceResponse<List<SupplierDto>>>($"api/Supplier/Suppliers");

            SupplierList = res.Success && res.Value != null ? res.Value : new List<SupplierDto>();
        }

        public async Task DeleteSupplier(Guid SupplierId)
        {
            var modalRes = await ModalManager.ConfirmationAsync("Confirm", "Supplier will be deleted. Are you sure?");
            if (!modalRes)
                return;

            try
            {
                var res = await Http.PostGetBaseResponseAsync("api/Supplier/DeleteSupplier", SupplierId);

                if (res.Success)
                {
                    SupplierList.RemoveAll(i => i.Id == SupplierId);
                    //await loadList();
                }
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("Error", ex.Message);
            }
        }

        public async void GoWebUrl(Uri Url)
        {
            await jsRuntime.InvokeAsync<object>("open", Url.ToString(), "_blank");
        }
    }
}
