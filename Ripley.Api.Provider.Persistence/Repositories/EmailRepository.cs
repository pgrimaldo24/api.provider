using Microsoft.EntityFrameworkCore;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Domain.Entities;
using Ripley.Api.Provider.Persistence.Context;

namespace Ripley.Api.Provider.Persistence.Repositories
{
    public class EmailRepository : GenericRepository<EmailEntity>, IEmailRepository
    {
        public EmailRepository(ProviderDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<EmailEntity> GetSenderEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Sender == email);
        }
    }
}
