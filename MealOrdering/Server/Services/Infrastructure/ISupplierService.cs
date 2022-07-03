using MealOrdering.Shared.Dtos;

namespace MealOrdering.Server.Services.Infrastructure
{
    public interface ISupplierService
    {
        Task<List<SupplierDto>> GetSuppliers();
        Task<SupplierDto> CreateSupplier(SupplierDto supplier);
        Task<SupplierDto> UpdateSupplier(SupplierDto supplier);
        Task<SupplierDto> GetSupplierById(Guid id);
        Task DeleteSupplier(Guid id);
    }
}
