
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace LoadTest
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", true, true)
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddHttpClient()
                .AddSingleton<LoadTestService>()
                .Configure<LoadTestOptions>(configuration)
                .BuildServiceProvider();
            
            var loadTest = serviceProvider.GetService<LoadTestService>();

            await loadTest.BeginTest();
        }
    }
}
