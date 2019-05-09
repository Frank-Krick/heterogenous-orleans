using Microsoft.Extensions.Hosting;
using SiloBase;

namespace Silo
{
    class Program
    {
        private static void Main(string[] args)
        {
            var host = CreateClusterHost();
            host.RunAsync().Wait();
        }

        private static IHost CreateClusterHost()
        {
            var hostBuilder = new HostBuilder()
                .UseOrleans(siloBuilder => { siloBuilder.ConfigureSilo(); });

            return hostBuilder.Build();
        }
    }
}
