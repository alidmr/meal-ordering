using Blazored.LocalStorage;
using MealOrdering.Client.Utilities;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.FilterModels;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;

namespace MealOrdering.Client.Pages.PageProcess
{
    public class OrderBusiness : ComponentBase
    {
        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }

        [Inject]
        protected ISyncLocalStorageService LocalStorageSync { get; set; }


        [Inject]
        ModalManager ModalManager { get; set; }

        public OrderListFilterModel FilterModel = new OrderListFilterModel();

        protected List<OrderDto> OrderList;

        internal bool Loading;

        protected override async Task OnInitializedAsync()
        {
            await ReLoadList();
        }

        protected String GetRemaningDateStr(DateTime expireDate)
        {
            TimeSpan ts = expireDate.Subtract(DateTime.Now);

            return ts.TotalSeconds >= 0 ? $"{ts.Hours}:{ts.Minutes}:{ts.Seconds}" : "00:00:00";
        }

        public void GoDetails(Guid selectedOrderId)
        {
            NavigationManager.NavigateTo("/orders-items/" + selectedOrderId.ToString());
        }


        public void GoCreateOrder()
        {
            NavigationManager.NavigateTo("/orders/add");
        }

        public void GoEditOrder(Guid orderId)
        {
            NavigationManager.NavigateTo("/orders/edit/" + orderId.ToString());
        }

        public async Task ReLoadList()
        {
            Loading = true;

            try
            {
                OrderList = await Http.PostGetServiceResponseAsync<List<OrderDto>, OrderListFilterModel>("api/Order/OrdersByFilter", FilterModel, true);
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("List Error", ex.Message);
            }
            finally
            {
                Loading = false;
            }
        }

        public bool IsExpired(DateTime expireDate)
        {
            TimeSpan ts = expireDate.Subtract(DateTime.Now);
            return ts.TotalSeconds < 0;
        }

        public async Task DeleteOrder(Guid orderId)
        {
            try
            {
                var modalRes = await ModalManager.ConfirmationAsync("Confirm", "Order will be deleted. Are you sure?");
                if (!modalRes)
                    return;

                var res = await Http.GetServiceResponseAsync<BaseResponse>("api/Order/DeleteOrder/" + orderId, true);

                OrderList.RemoveAll(i => i.Id == orderId);
            }
            catch (ApiException ex)
            {
                await ModalManager.ShowMessageAsync("Deletion Error", ex.Message);
            }
        }

        public bool IsMyOrder(Guid createdUserId)
        {
            return LocalStorageSync.GetUserIdSync() == createdUserId;
        }
    }
}
