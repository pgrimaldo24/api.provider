using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.Persistence.Context;

namespace Ripley.Api.Provider.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProviderDbContext _dbContext;

        private IUserRepository _userRepository;
        private IRolRepository _rolRepository;
        private IProviderRepository _providerRepository;
        private IEmailRepository _emailRepository;
        private IEmailHistoryRepository _emailHistoryRepository;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private ISucursalRepository _sucursalRepository;

        public UnitOfWork(ProviderDbContext dbContext,
            IUserRepository userRepository,
            IRolRepository rolRepository,
            IProviderRepository providerRepository,
            IEmailRepository emailRepository,
            IEmailHistoryRepository emailHistoryRepository,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ISucursalRepository sucursalRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _rolRepository = rolRepository;
            _providerRepository = providerRepository;
            _emailRepository = emailRepository;
            _emailHistoryRepository = emailHistoryRepository;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _sucursalRepository = sucursalRepository;
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_dbContext);

                return this._userRepository;
            }
        }

        public IRolRepository RolRepository
        {
            get
            {
                _rolRepository ??= new RolRepository(_dbContext);

                return this._rolRepository;
            }
        }

        public ISucursalRepository SucursalRepository
        {
            get
            {
                _sucursalRepository ??= new SucursalRepository(_dbContext);

                return this._sucursalRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                _productRepository ??= new ProductRepository(_dbContext);

                return this._productRepository;
            }
        }

        public ICategoryRepository CategoryRepository
        {
            get
            {
                _categoryRepository ??= new CategoryRepository(_dbContext);

                return this._categoryRepository;
            }
        }

        public IProviderRepository ProviderRepository
        {
            get
            {
                _providerRepository ??= new ProviderRepository(_dbContext);

                return this._providerRepository;
            }
        }

        public IEmailRepository EmailRepository
        {
            get
            {
                _emailRepository ??= new EmailRepository(_dbContext);

                return this._emailRepository;
            }
        }

        public IEmailHistoryRepository EmailHistoryRepository
        {
            get
            {
                _emailHistoryRepository ??= new EmailHistoryRepository(_dbContext);

                return this._emailHistoryRepository;
            }
        }
        public async Task<int> CompletedAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
