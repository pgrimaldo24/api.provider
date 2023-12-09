using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity> ValidationCredentialsAsync(string user, string password);
    }
}
