using System.Security.Claims;
using MealOrdering.Server.Services.Infrastructure;

namespace MealOrdering.Server.Services.Service
{
    public class ValidationService : IValidationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasPermission(Guid userId)
        {
            var item = IsAdmin(userId) && HasPermissionToChange(userId);
            return item;
        }

        public bool HasPermissionToChange(Guid userId)
        {
            String item = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData).Value;

            return Guid.TryParse(item, out Guid result) ? result == userId : false;
        }

        public bool IsAdmin(Guid userId)
        {
            var item = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value == "alidemirytu@gmail.com";
            return item;
        }
    }
}
