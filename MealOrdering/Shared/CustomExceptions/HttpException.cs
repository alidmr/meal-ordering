namespace MealOrdering.Shared.CustomExceptions
{
    public class HttpException : Exception
    {
        public HttpException(string message) : base(message) { }

        public HttpException(string message, Exception innerException) : base(message, innerException) { }
    }
}
