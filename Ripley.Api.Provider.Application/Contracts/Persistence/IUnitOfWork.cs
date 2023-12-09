namespace Ripley.Api.Provider.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; } 
        IRolRepository RolRepository { get; }
        IProviderRepository ProviderRepository { get; } 
        IEmailRepository EmailRepository { get; }
        IEmailHistoryRepository EmailHistoryRepository { get; }
        Task<int> CompletedAsync();
    }
}
