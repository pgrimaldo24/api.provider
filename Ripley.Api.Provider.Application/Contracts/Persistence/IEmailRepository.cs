using Ripley.Api.Provider.Domain.Entities;

namespace Ripley.Api.Provider.Application.Contracts.Persistence
{
    public interface IEmailRepository : IGenericRepository<EmailEntity>
    {
        Task<EmailEntity> GetSenderEmailAsync(string email);
    }
}
