using MealOrdering.Shared.Dtos;

namespace MealOrdering.Server.Services.Infrastructure
{
    public interface IUserService
    {
        public Task<UserDto> GetUserById(Guid id);
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> CreateUser(UserDto user);
        public Task<UserDto> UpdateUser(UserDto user);
        public Task<bool> DeleteUserById(Guid id);

        public Task<UserLoginResponseDto> Login(string email, string password);
    }
}
