using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TennisExplorer.Infrastructure
{
    public static class Bootstrapper
    {
        public async static Task InitializeApplicationDependencies(IServiceCollection services)
        {
            await Task.Run(async () => 
            {
                AppDependencySetup.ConfigureDependencies(services);
                await InitializeDatabase(); 
            });
        }

        private static Task InitializeDatabase()
        {
            return Task.Delay(2000);
        }
    }
}
