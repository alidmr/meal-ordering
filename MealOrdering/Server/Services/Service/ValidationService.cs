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

        public bool HasPermission(Guid UserId)
        {
            return IsAdmin(UserId) || HasPermissionToChange(UserId);
        }

        public bool HasPermissionToChange(Guid UserId)
        {
            String userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData).Value;

            return Guid.TryParse(userId, out Guid result) ? result == UserId : false;
        }

        public bool IsAdmin(Guid UserId)
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value == "alidemirytu@gmail.com";
        }
    }
}
