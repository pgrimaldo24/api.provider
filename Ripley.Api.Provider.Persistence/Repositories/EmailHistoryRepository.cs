using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Domain.Entities;
using Ripley.Api.Provider.Persistence.Context;

namespace Ripley.Api.Provider.Persistence.Repositories
{
    public class EmailHistoryRepository : GenericRepository<EmailHistoryEntity>, IEmailHistoryRepository
    {
        public EmailHistoryRepository(ProviderDbContext dbContext) : base(dbContext)
        {
        }
    }
}
