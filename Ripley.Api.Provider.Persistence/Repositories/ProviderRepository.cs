using Microsoft.EntityFrameworkCore;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Domain.Entities;
using Ripley.Api.Provider.Persistence.Context;

namespace Ripley.Api.Provider.Persistence.Repositories
{
    public class ProviderRepository : GenericRepository<ProviderEntity>, IProviderRepository
    {
        public ProviderRepository(ProviderDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ProviderEntity> GetProviderByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
