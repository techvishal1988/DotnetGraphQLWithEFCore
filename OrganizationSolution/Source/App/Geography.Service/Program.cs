namespace Geography.Service
{
    using Framework.Configuration;
    using Framework.Configuration.Models;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="Program" />.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();

        }

        /// <summary>
        /// The CreateHostBuilder.
        /// </summary>
        /// <param name="args">The args<see cref="string[]"/>.</param>
        /// <returns>The <see cref="IHostBuilder"/>.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .DefaultAppConfiguration(new[] { typeof(ApplicationOptions).Assembly, typeof(SecurityOptions).Assembly }, args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
