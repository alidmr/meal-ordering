using Blazored.Modal;
using Blazored.Modal.Services;
using MealOrdering.Client.CustomComponents.Modals;

namespace MealOrdering.Client.Utilities
{
    public class ModalManager
    {
        private readonly IModalService _modalService;

        public ModalManager(IModalService modalService)
        {
            _modalService = modalService;
        }

        public async Task ShowMessageAsync(string title, string message, int duration = 0)
        {
            ModalParameters modalParameters = new ModalParameters();
            modalParameters.Add("Message", message);

            var modalRef = _modalService.Show<ShowMessagePopupComponent>(title, modalParameters);

            if (duration > 0)
            {
                await Task.Delay(duration);
                modalRef.Close();
            }
        }

        public async Task<bool> ConfirmationAsync(string title, string message)
        {
            ModalParameters modalParameters = new ModalParameters();
            modalParameters.Add("Message", message);

            var modalRef = _modalService.Show<ConfirmationPopupComponent>(title, modalParameters);
            var modalResult = await modalRef.Result;

            return !modalResult.Cancelled;
        }
    }
}
