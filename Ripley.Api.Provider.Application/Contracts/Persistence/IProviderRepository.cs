using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Application.Contracts.Persistence
{
    public interface IProviderRepository : IGenericRepository<ProviderEntity>
    {
        Task<ProviderEntity> GetProviderByEmailAsync(string email);
    }
}
