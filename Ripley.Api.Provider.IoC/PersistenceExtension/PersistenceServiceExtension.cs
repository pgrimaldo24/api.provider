using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ripley.Api.Provider.Application.Contracts.Infraestructure;
using Ripley.Api.Provider.Application.Contracts.Persistence;
using Ripley.Api.Provider.CrossCutting.Configuration;
using Ripley.Api.Provider.Infraestructure.MailServer;
using Ripley.Api.Provider.Persistence.Context;
using Ripley.Api.Provider.Persistence.Repositories;

namespace Ripley.Api.Provider.IoC.PersistenceExtension
{
    public static class PersistenceServiceExtension
    {
        public static IServiceCollection AddPersistenceServiceRegistrationExtension(this IServiceCollection services, IConfiguration configuration, AppSetting appSetting)
        {
            services.AddDbContext<ProviderDbContext>(options =>
                    options.UseSqlServer(String.Format("Data Source={0};Initial Catalog={1};Trusted_Connection=True;TrustServerCertificate=True", appSetting.ConnectionStrings.DataSource, appSetting.ConnectionStrings.Catalog)));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IProviderRepository, ProviderRepository>();
            services.AddScoped<IMailServerService, MailServerService>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISucursalRepository, SucursalRepository>();

            return services;
        }
    }
}
