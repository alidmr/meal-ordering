namespace MealOrdering.Shared.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpireDate { get; set; }

        public string CreatedUserFullName { get; set; }
        public string SupplierName { get; set; }
    }
}
