using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
        Task<List<ProductEntity>> GetProductsByMerchantId(int id);
    }
}
