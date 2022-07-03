namespace MealOrdering.Shared.Dtos
{
    public class UserLoginResponseDto
    {
        public string ApiToken { get; set; }

        public UserDto User { get; set; }
    }
}
