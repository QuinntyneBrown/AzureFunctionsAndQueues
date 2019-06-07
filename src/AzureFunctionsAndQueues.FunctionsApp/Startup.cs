using AzureFunctionsAndQueues.FunctionsApp;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctionsAndQueues.FunctionApp.Startup))]
namespace AzureFunctionsAndQueues.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
            => builder.Services.ConfigureServices();
    }
}
