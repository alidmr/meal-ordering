using Blazored.LocalStorage;

namespace MealOrdering.Client.Utilities
{
    public static class LocalStorageExtension
    {

        public static async Task<Guid> GetUserId(this ILocalStorageService localStorage)
        {
            //String userGuid = await localStorage.GetItemAsStringAsync("UserId");

            var item = await localStorage.GetItemAsync<string>("UserId");

            return Guid.TryParse(item, out Guid userId) ? userId : Guid.Empty;
        }

        public static Guid GetUserIdSync(this ISyncLocalStorageService localStorage)
        {
            //string userGuid = localStorage.GetItemAsString("UserId");

            var item = localStorage.GetItem<string>("UserId");

            return Guid.TryParse(item, out Guid userId) ? userId : Guid.Empty;
        }

        public static async Task<String> GetUserEMail(this ILocalStorageService localStorage)
        {
            var item = await localStorage.GetItemAsync<string>("email");

            //var item = await localStorage.GetItemAsStringAsync("email");

            return item;
        }

        public static async Task<String> GetUserFullName(this ILocalStorageService localStorage)
        {
            var item = await localStorage.GetItemAsync<string>("UserFullName");

            //var item = await localStorage.GetItemAsStringAsync("UserFullName");

            return item;
        }
    }
}
