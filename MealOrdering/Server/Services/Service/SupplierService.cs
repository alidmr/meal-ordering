using AutoMapper;
using AutoMapper.QueryableExtensions;
using MealOrdering.Server.Data.Context;
using MealOrdering.Server.Data.Models;
using MealOrdering.Server.Services.Infrastructure;
using MealOrdering.Shared.Dtos;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Services.Service
{
    public class SupplierService : ISupplierService
    {
        private readonly IMapper _mapper;
        private readonly MealOrderingDbContext _context;

        public SupplierService(IMapper mapper, MealOrderingDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<SupplierDto>> GetSuppliers()
        {
            return await _context.Suppliers
                .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.CreateDate)
                .ToListAsync();
        }

        public async Task<SupplierDto> CreateSupplier(SupplierDto supplier)
        {
            var dbSupplier = _mapper.Map<Supplier>(supplier);
            await _context.Suppliers.AddAsync(dbSupplier);
            await _context.SaveChangesAsync();

            return _mapper.Map<SupplierDto>(dbSupplier);
        }

        public async Task<SupplierDto> UpdateSupplier(SupplierDto supplier)
        {
            var dbSupplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == supplier.Id);
            if (dbSupplier == null)
                throw new Exception("Restorant bulunamadı");

            _mapper.Map(supplier, dbSupplier);
            await _context.SaveChangesAsync();

            return _mapper.Map<SupplierDto>(dbSupplier);
        }

        public async Task<SupplierDto> GetSupplierById(Guid id)
        {
            return await _context.Suppliers.Where(x => x.Id == id)
                .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteSupplier(Guid id)
        {
            var supplier = await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id);
            if (supplier == null)
                throw new Exception("Restorant bulunamadı");

            int orderCount = await _context.Suppliers.Include(x => x.Orders).Select(x => x.Orders.Count)
                .FirstOrDefaultAsync();

            if (orderCount > 0)
                throw new Exception($"Silmeye çalıştığınız restorant için oluşturulmuş {orderCount} adet sipariş mevcut");

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();
        }
    }
}
