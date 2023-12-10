using Microsoft.EntityFrameworkCore;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Domain.Entities;
using Ripley.Api.Provider.Persistence.Context;

namespace Ripley.Api.Provider.Persistence.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(ProviderDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<UserEntity> ValidationCredentialsAsync(string user, string password)
        {
            return await this._dbSet
                    .FirstOrDefaultAsync(x => x.User == user
                                           && x.Password == password);
        }
    }
}
