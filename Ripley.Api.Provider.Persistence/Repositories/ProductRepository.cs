using Microsoft.EntityFrameworkCore;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Domain.Entities;
using Ripley.Api.Provider.Persistence.Context;

namespace Ripley.Api.Provider.Persistence.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
    { 
        public ProductRepository(ProviderDbContext dbContext) : base(dbContext)
        { 
        }

        public async Task<List<ProductEntity>> GetProductsByMerchantId(int id)
        {
            return await _dbSet.Where(x => x.MerchantId == id).ToListAsync();
        }
    }
}
