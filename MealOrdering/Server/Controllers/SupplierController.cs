using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.Dtos;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService SupplierService)
        {
            _supplierService = SupplierService;
        }



        [HttpGet("SupplierById/{Id}")]
        public async Task<ServiceResponse<SupplierDto>> GetSupplierById(Guid Id)
        {
            return new ServiceResponse<SupplierDto>()
            {
                Value = await _supplierService.GetSupplierById(Id)
            };
        }


        [HttpGet("Suppliers")]
        public async Task<ServiceResponse<List<SupplierDto>>> GetSuppliers()
        {
            return new ServiceResponse<List<SupplierDto>>()
            {
                Value = await _supplierService.GetSuppliers()
            };
        }


        [HttpPost("CreateSupplier")]
        public async Task<ServiceResponse<SupplierDto>> CreateSupplier(SupplierDto Supplier)
        {
            return new ServiceResponse<SupplierDto>()
            {
                Value = await _supplierService.CreateSupplier(Supplier)
            };
        }


        [HttpPost("UpdateSupplier")]
        public async Task<ServiceResponse<SupplierDto>> UpdateSupplier(SupplierDto Supplier)
        {
            return new ServiceResponse<SupplierDto>()
            {
                Value = await _supplierService.UpdateSupplier(Supplier)
            };
        }


        [HttpPost("DeleteSupplier")]
        public async Task<BaseResponse> DeleteSupplier([FromBody] Guid SupplierId)
        {
            await _supplierService.DeleteSupplier(SupplierId);
            return new BaseResponse();
        }
    }
}
