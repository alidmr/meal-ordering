using System.ComponentModel.DataAnnotations;

namespace MealOrdering.Shared.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateUserId { get; set; }
        public Guid SupplierId { get; set; }

        [MinLength(3, ErrorMessage = "Name Min. 3 karakter olmalıdır")]
        [StringLength(15, ErrorMessage = "Name Max. 15 karakter olmalıdır")]
        public string Name { get; set; }

        [MinLength(10)]
        [StringLength(100)]
        public string Description { get; set; }

        public DateTime ExpireDate { get; set; }

        public string? CreatedUserFullName { get; set; }
        public string? SupplierName { get; set; }
    }
}
