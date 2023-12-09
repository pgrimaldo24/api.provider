using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ripley.Api.Provider.IoC.ApplicationExtension
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServiceRegistrationExtension(this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies())); 
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();  
            return services;
        }
    }
}
