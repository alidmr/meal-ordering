namespace MealOrdering.Shared.Dtos
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid OrderId { get; set; }
        public string Description { get; set; }
        public string OrderName { get; set; }
        public string CreatedUserFullName { get; set; }
    }
}
