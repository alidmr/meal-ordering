using Blazored.LocalStorage;

namespace MealOrdering.Client.Utilities
{
    public static class LocalStorageExtension
    {

        public static async Task<Guid> GetUserId(this ILocalStorageService LocalStorage)
        {
            String userGuid = await LocalStorage.GetItemAsStringAsync("UserId");

            return Guid.TryParse(userGuid, out Guid UserId) ? UserId : Guid.Empty;
        }

        public static Guid GetUserIdSync(this ISyncLocalStorageService LocalStorage)
        {
            String userGuid = LocalStorage.GetItemAsString("UserId");

            return Guid.TryParse(userGuid, out Guid UserId) ? UserId : Guid.Empty;
        }

        public static async Task<String> GetUserEMail(this ILocalStorageService LocalStorage)
        {
            return await LocalStorage.GetItemAsStringAsync("email");
        }

        public static async Task<String> GetUserFullName(this ILocalStorageService LocalStorage)
        {
            return await LocalStorage.GetItemAsStringAsync("UserFullName");
        }
    }
}
