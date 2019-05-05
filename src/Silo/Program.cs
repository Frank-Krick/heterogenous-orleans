using System;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Statistics;

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
                .UseOrleans(siloBuilder =>
                {
                    var connectionStringBuilder = new NpgsqlConnectionStringBuilder();
                    connectionStringBuilder.Host = "orleans-postgres";
                    connectionStringBuilder.Database = "orleans";
                    connectionStringBuilder.Username = "orleans";
                    connectionStringBuilder.Password = "rewgpionevofrrrfgjjj";
                    siloBuilder.Configure((Action<ClusterOptions>) (options =>
                    {
                      options.ClusterId = "heterogenous-test-cluster";
                      options.ServiceId = "heterogenous-test-cluster";
                    }));
                    siloBuilder.ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000);
                    siloBuilder.AddAdoNetGrainStorage("grain-storage", options =>
                    {
                        options.Invariant = "Npgsql";
                        options.ConnectionString = connectionStringBuilder.ConnectionString;
                    });

                    siloBuilder.UseAdoNetClustering(options =>
                    {
                        options.Invariant = "Npgsql";
                        options.ConnectionString = connectionStringBuilder.ConnectionString;
                    });

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        siloBuilder.UseLinuxEnvironmentStatistics();
                    }

                    siloBuilder.UseDashboard(options => { options.Port = 1337; });
                });

            return hostBuilder.Build();
        }
    }
}
