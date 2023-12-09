
using Ripley.Api.Provider.Configuration.Startup;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel();
var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();