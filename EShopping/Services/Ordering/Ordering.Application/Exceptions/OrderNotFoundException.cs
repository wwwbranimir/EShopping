namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : ApplicationException
    {
        public OrderNotFoundException()
        {
        }
        public OrderNotFoundException(string? name, object key) : base($"entity {name} - {key} was not found ")
        {
        }
        public OrderNotFoundException(string? message) : base(message)
        {
        }

        public OrderNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}