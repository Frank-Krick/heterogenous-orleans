using System;
using System.Runtime.InteropServices;
using Npgsql;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Statistics;

namespace SiloBase
{
    public static class ConfigExtensions
    {
        public static ISiloBuilder ConfigureSilo(this ISiloBuilder builder)
        {
                    var connectionStringBuilder = new NpgsqlConnectionStringBuilder
                    {
                        Host = "orleans-postgres",
                        Database = "orleans",
                        Username = "orleans",
                        Password = "rewgpionevofrrrfgjjj"
                    };
                    builder.Configure((Action<ClusterOptions>) (options =>
                    {
                      options.ClusterId = "heterogenous-test-cluster";
                      options.ServiceId = "heterogenous-test-cluster";
                    }));
                    builder.ConfigureEndpoints(11111, 30000);
                    builder.AddAdoNetGrainStorage("grain-storage", options =>
                    {
                        options.Invariant = "Npgsql";
                        options.ConnectionString = connectionStringBuilder.ConnectionString;
                    });

                    builder.UseAdoNetClustering(options =>
                    {
                        options.Invariant = "Npgsql";
                        options.ConnectionString = connectionStringBuilder.ConnectionString;
                    });

                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        builder.UseLinuxEnvironmentStatistics();
                    }

                    builder.UseDashboard(options => { options.Port = 1337; });

            return builder;
        }
    }
}
