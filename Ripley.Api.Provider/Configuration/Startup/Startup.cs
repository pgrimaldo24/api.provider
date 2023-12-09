using Microsoft.Extensions.Options;
using Ripley.Api.Provider.CrossCutting.Configuration;
using Ripley.Api.Provider.CrossCutting.Configuration.Services;
using Ripley.Api.Provider.CrossCutting.Extensions.Swagger;
using Ripley.Api.Provider.IoC.ApplicationExtension;
using Ripley.Api.Provider.IoC.PersistenceExtension;
using static Ripley.Api.Provider.CrossCutting.Resoures.ServicesConstants;

namespace Ripley.Api.Provider.Configuration.Startup
{
    public static class Startup
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var appSettings = builder.Configuration.GetSection("AppSettings");

            builder.Services.Configure<AppSetting>(appSettings);

            builder.Services.AddSingleton(x => x.GetService<IOptions<AppSetting>>().Value);

            var appSetting = appSettings.Get<AppSetting>();
            
            builder.Services.AddControllers();
            builder.Services.AddOptions();

            builder.Services.AddMvc();

            builder.Services.AddApplicationServiceRegistrationExtension(builder.Configuration);
            builder.Services.AddPersistenceServiceRegistrationExtension(builder.Configuration, appSetting); 

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerExtension(ClientService.APIName);

            builder.Services.AddCors(options =>
                options.AddPolicy("corsapp",
                builder =>
                {
                    builder.WithOrigins("*")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                }
            ));

            builder.Services.AddHttpClient<HttpClientService>().ConfigureHttpMessageHandlerBuilder(http =>
            {
                http.PrimaryHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
                };
            });

            builder.Services.AddMemoryCache();
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
           );

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("corsapp");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                x.DocumentTitle = ClientName.NameService;
            });

            app.Run();

            return app;
        }
    } 
}
